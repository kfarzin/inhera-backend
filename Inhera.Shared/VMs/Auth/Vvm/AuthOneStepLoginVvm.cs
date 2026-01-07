using System.ComponentModel.DataAnnotations;

namespace Inhera.Shared.VMs.Auth.Vvm
{
    public class AuthOneStepLoginVvm
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Code is required")]
        public required string Code { get; set; }
    }
}
