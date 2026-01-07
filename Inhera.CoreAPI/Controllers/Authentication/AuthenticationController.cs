
using Inhera.CoreAPI.Controllers.Base;
using Inhera.CoreAPI.ResponderServices;
using Inhera.Shared.Models.Common;
using Inhera.Shared.Util.Logging;
using Inhera.Shared.VMs.Auth.Vm;
using Inhera.Shared.VMs.Auth.Vvm;
using Microsoft.AspNetCore.Mvc;


namespace CoreAPI.Controllers
{
    [ApiController]
    public class AuthenticationController : CoreAPIController
    {
        private readonly AuthResponderService authResponderService;
        private readonly ConsoleLogger logger;

        public AuthenticationController(            
            AuthResponderService authResponderService,
            ConsoleLogger consoleLogger
            )
        {
            logger = consoleLogger;
            this.authResponderService = authResponderService;
        }

        [HttpPost("code")]
        public async Task<ActionResult<StandardContainer<AuthRegisterVm>>> RegisterWithOnlyEmail(AuthOneStepRegisterVvm model)
        {
            return CreatedContainer(await authResponderService.RegisterWithOnlyEmail(model));
        }

        [HttpPost("login")]
        public async Task<ActionResult<StandardContainer<AuthRegisterVm>>> LoginWithCode(AuthOneStepLoginVvm model)
        {
            return CreatedContainer(await authResponderService.LoginWithCode(model));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<StandardContainer<AuthRegisterVm>>> GetTokenByRefreshToken(AuthRefreshTokenVvm model)
        {
            return OkContainer(await authResponderService.GetTokenByRefreshToken(model));
        }
    }
}
