using Inhera.Authentication.Models;
using Inhera.CoreAPI.Data;
using Inhera.Shared.Models.Database.SQL.Entities;
using Inhera.Shared.Repositories;
using Inhera.Shared.Services;
using Inhera.Shared.Util.Address;
using Inhera.Shared.Util.Extensions;
using Inhera.Shared.VMs.Auth.Vvm;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Inhera.CoreAPI.Services
{
    public class AuthenticationService : SqlService<UserProfileEntity, CoreContext> //ServiceResult
    {
        const int REFRESH_TOKEN_VALIDITY_IN_DAYS = 90;
        const int LOGIN_CODE_VALIDITY_IN_MINUTES = 5;
        const int TOKEN_EXPIRATION_IN_DAYS = 1;        
        private readonly UserManager<AuthUser> _userManager;        
        private readonly IConfiguration _configuration;
        public AuthenticationService(
        SqlRepository<UserProfileEntity, CoreContext> repository,
        UserManager<AuthUser> userManager,        
        IConfiguration configuration) : base(repository)
        {
            _userManager = userManager;
            _configuration = configuration;            
            //_mapper = mapper;
        }

        public async Task<(string, string)> LoginWithCode(AuthOneStepLoginVvm model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.LoginCodeExpiryDateTime < DateTimeOffset.UtcNow)
                {
                    //TODO
                    throw new Exception();
                }
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (user.LoginCode == model.Code && user.LoginCode != string.Empty)
                    {
                        user.LoginCode = string.Empty;
                        await _userManager.UpdateAsync(user);
                        var claimsIdentity = await GetClaimsIdentityForUser(user);
                        var refreshToken = GenerateBase64RandomByteArray();
                        user.RefreshToken = refreshToken;
                        user.RefreshTokenExpiryDateTime = GetExpirationDate(REFRESH_TOKEN_VALIDITY_IN_DAYS);
                        await _userManager.UpdateAsync(user);
                        var token = GenerateToken(user.UserName!, claimsIdentity);
                        scope.Complete();
                        return (token!, refreshToken);
                    }
                }
            }
            //TODO: return error
            return (null!, null!);
        }

        public async Task<(string, string)> GetTokenByRefreshToken(AuthRefreshTokenVvm model)
        {
            //must be revised from security perspective
            var principals = GetPrincipalFromExpiredToken(model.Token);
            var emailClaim = principals.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email.ToString());
            if (emailClaim == null)
            {
                //TODO
                throw new Exception();
            }

            var email = emailClaim.Value;
            var user = await _userManager.FindByEmailAsync(email);
            //TODO check the validity of refresh token
            if (user != null && user.RefreshToken == model.RefreshToken)
            {
                var claimsIdentity = await GetClaimsIdentityForUser(user);
                var refreshToken = GenerateBase64RandomByteArray();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryDateTime = GetExpirationDate(REFRESH_TOKEN_VALIDITY_IN_DAYS);
                await _userManager.UpdateAsync(user);
                var token = GenerateToken(user.UserName!, claimsIdentity);
                return (token!, refreshToken);

            }
            return (null!, null!);
        }

        public async Task<string> RegisterWithOnlyEmail(AuthOneStepRegisterVvm model)
        {
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            var loginCode = GenerateLoginCode();
            
            if (existingUser != null)
            {
                existingUser.LoginCode = loginCode;
                existingUser.LoginCodeExpiryDateTime = GetExpirationDateForLoginCode(LOGIN_CODE_VALIDITY_IN_MINUTES);
                await _userManager.UpdateAsync(existingUser);
                return loginCode;
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = new AuthUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    RefreshToken = GenerateBase64RandomByteArray(),
                    RefreshTokenExpiryDateTime = GetExpirationDate(REFRESH_TOKEN_VALIDITY_IN_DAYS),
                    LoginCode = loginCode,
                    LoginCodeExpiryDateTime = GetExpirationDateForLoginCode(LOGIN_CODE_VALIDITY_IN_MINUTES),
                };

                var result = await _userManager.CreateAsync(user);

                var address = AddressUtil.CreateAnEmptyAddressWithType(Shared.Enums.AddressTypes.Personal);

                // should be refactored
                var companyCode = model.Email.Substring(0, 1) + model.Email.Substring(1, 1);

                var userProfile = new UserProfileEntity
                {                    
                    AuthId = user.Id,
                    Email = model.Email,
                    //TODO: to check whether there should be a strategy for customer number
                    CustomerNumber = Guid.NewGuid().ToString(),    
                    Address = address,
                };

                await _repository.Add(userProfile);                

                if (result.Succeeded)
                {
                    await AddDefaultClaims(user, userProfile);
                    //await AddAdminClaim(user);
                    //await AddSellerSideClaim(user);
                    var claimsIdentity = await GetClaimsIdentityForUser(user);
                    scope.Complete();
                    var token = GenerateToken(user.UserName, claimsIdentity);
                    var refreshToken = user.RefreshToken;
                    return loginCode;
                }
                else
                {
                    return "";
                }                
            }
        }

        public async Task<(string, string)> Login(AuthLoginVvm model, Func<string> resetCheckoutSession)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var claimsIdentity = await GetClaimsIdentityForUser(user);
                        var refreshToken = GenerateBase64RandomByteArray();
                        user.RefreshToken = refreshToken;
                        user.RefreshTokenExpiryDateTime = GetExpirationDate(REFRESH_TOKEN_VALIDITY_IN_DAYS);
                        await _userManager.UpdateAsync(user);
                        var token = GenerateToken(user.UserName!, claimsIdentity);
                        scope.Complete();
                        return (token!, refreshToken);
                    }
                }
            }
            //TODO: return error
            return (null!, null!);
        }
        private string? GenerateToken(string userName, ClaimsIdentity claimsIdentity)
        {
            var secret = _configuration["JwtConfig:Secret"];
            var issuer = _configuration["JwtConfig:ValidIssuer"];
            var audience = _configuration["JwtConfig:ValidAudiences"];
            if (secret is null || issuer is null || audience is null)
            {
                throw new ApplicationException("Jwt is not set in the configuration");
            }
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(TOKEN_EXPIRATION_IN_DAYS),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            //var jwtToken = new JwtSecurityToken(
            //    issuer: issuer,
            //    audience: audience,
            //    claims: new[]{
            //        new Claim(ClaimTypes.Name, userName)
            //    },
            //    expires: DateTime.UtcNow.AddDays(1),
            //    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            //);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
        
        private string GenerateLoginCode()
        {
            return new Random().Next(1000, 9999).ToString();
        }

        private DateTime GetExpirationDate(int days)
        {
            return DateTime.Now.AddDays(days).ToUniversalTime();
        }

        private DateTime GetExpirationDateForLoginCode(int minutes)
        {
            return DateTime.Now.AddMinutes(minutes).ToUniversalTime();
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var secret = _configuration["JwtConfig:Secret"];
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        public string GenerateBase64RandomByteArray()
        {
            var randomByteArray = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomByteArray);
                return Convert.ToBase64String(randomByteArray);
            }
        }
        private async Task AddDefaultClaims(AuthUser user, UserProfileEntity profile)
        {
            await _userManager.AddClaimsAsync(user, [
                new Claim(ClaimTypes.Email, user.Email!)
                ]);
            await _userManager.AddClaimsAsync(user, [
                new Claim(ClaimTypes.NameIdentifier, profile.Id.ToString()!)
                ]);
        }
        private async Task AddAdminClaim(AuthUser user)
        {
            //await _userManager.AddClaimsAsync(user, [
            //    new Claim(AuthClaims.SIDES, AuthClaims.BUY_SIDE)
            //    ]);
        }
        
        private ClaimsIdentity ToClaimIdentity(IList<Claim> claims)
        {
            return new ClaimsIdentity(claims.Select(e => new Claim(e.Type, e.Value)));
        }
        private async Task<IList<Claim>> GetUserClaims(AuthUser user)
        {
            return await _userManager.GetClaimsAsync(user);
        }
        private async Task<ClaimsIdentity> GetClaimsIdentityForUser(AuthUser user)
        {
            return ToClaimIdentity(await GetUserClaims(user));
        }
    }
}
