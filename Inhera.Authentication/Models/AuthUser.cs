using Microsoft.AspNetCore.Identity;

namespace Inhera.Authentication.Models
{
    public class AuthUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryDateTime { get; set; }
        public string LoginCode { get; set; } = string.Empty;
        public DateTimeOffset LoginCodeExpiryDateTime { get; set; }
    }
}
