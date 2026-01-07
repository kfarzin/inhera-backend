using System.ComponentModel.DataAnnotations;

namespace Inhera.Shared.VMs.Auth.Vvm
{
    public class AuthOneStepRegisterVvm
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
    }
}
