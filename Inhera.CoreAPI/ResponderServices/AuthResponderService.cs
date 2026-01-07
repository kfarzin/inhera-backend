

using DotNetCore.CAP;
using Inhera.CoreAPI.Services;
using Inhera.Shared.Models.Common;
using Inhera.Shared.Models.DomainEvents;
using Inhera.Shared.Services;
using Inhera.Shared.VMs.Auth.Vm;
using Inhera.Shared.VMs.Auth.Vvm;

namespace Inhera.CoreAPI.ResponderServices
{
    public class AuthResponderService : BaseResponderService
    {
        private readonly AuthenticationService AuthenticationService;
        private readonly ICapPublisher cap;
        public AuthResponderService(AuthenticationService AuthenticationService, ICapPublisher cap)
        {
            this.AuthenticationService = AuthenticationService;
            this.cap = cap;
        }

        public async Task<ServiceContainer<AuthRegisterVm>> LoginWithCode(AuthOneStepLoginVvm model)
        {
            try
            {
                var (token, refreshToken) = await AuthenticationService.LoginWithCode(model);
                return OkContainer(new AuthRegisterVm
                {
                    Token = token!,
                    RefreshToken = refreshToken,
                });
            }
            catch (Exception ex)
            {
                return GenerateErrorResponse<AuthRegisterVm>(ex);
            }
        }       

        public async Task<ServiceContainer<SingleFieldResponse<string>>> RegisterWithOnlyEmail(AuthOneStepRegisterVvm model)
        {
            try
            {
                var loginCode = await AuthenticationService.RegisterWithOnlyEmail(model);
                await cap.PublishAsync(ChannelNames.Generic, new AccountRegistrationDomainEvent { 
                    Email = model.Email, 
                    Code = loginCode 
                });
                
                return OkContainer(SingleFieldResponse<string>.Of(""));
            }
            catch (Exception ex)
            {
                return GenerateErrorResponse<SingleFieldResponse<string>>(ex);
            }
        }

        public async Task<ServiceContainer<AuthRegisterVm>> Login(AuthLoginVvm model, Func<string> resetCheckoutSession)
        {
            try
            {
                var (token, refreshToken) = await AuthenticationService.Login(model, resetCheckoutSession);
                return OkContainer(new AuthRegisterVm
                {
                    Token = token!,
                    RefreshToken = refreshToken,
                });
            }
            catch (Exception ex)
            {
                return GenerateErrorResponse<AuthRegisterVm>(ex);
            }
        }

        public async Task<ServiceContainer<AuthRegisterVm>> GetTokenByRefreshToken(AuthRefreshTokenVvm model)
        {
            try
            {
                var (token, refreshToken) = await AuthenticationService.GetTokenByRefreshToken(model);
                return OkContainer(new AuthRegisterVm
                {
                    Token = token!,
                    RefreshToken = refreshToken,
                });
            }
            catch (Exception ex)
            {
                return GenerateErrorResponse<AuthRegisterVm>(ex);
            }
        }
    }
}
