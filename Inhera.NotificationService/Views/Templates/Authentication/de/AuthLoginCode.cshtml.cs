using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inhera.NotificationService.Views.Templates.Authentication.de
{
    public class AuthLoginCodeModel : PageModel
    {
        public required string Code { get; set; }
        public void OnGet()
        {
        }
    }
}
