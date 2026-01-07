using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inhera.NotificationService.Views.Templates.Authentication.en
{
    public class AuthLoginCodeModel : PageModel
    {
        public required string Code { get; set; }
        public void OnGet()
        {
        }
    }
}
