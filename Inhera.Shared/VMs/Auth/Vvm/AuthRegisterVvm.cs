using System.ComponentModel.DataAnnotations;

namespace Inhera.Shared.VMs.Auth.Vvm
{
    public class AuthRegisterVvm
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
