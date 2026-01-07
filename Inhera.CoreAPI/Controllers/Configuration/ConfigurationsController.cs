using Inhera.CoreAPI.Controllers.Base;
using Inhera.CoreAPI.ResponderServices;
using Inhera.Shared.Models.Common;
using Inhera.Shared.Util.Logging;
using Inhera.Shared.VMs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inhera.CoreAPI.Controllers.Configuration
{
    [Authorize]
    [ApiController]
    public class ConfigurationsController : CoreAPIController
    {
        private readonly ConfigurationResponderService configurationResponderService;
        private readonly ConsoleLogger logger;

        public ConfigurationsController(
            ConfigurationResponderService configurationResponderService,
            ConsoleLogger consoleLogger)
        {
            logger = consoleLogger;
            this.configurationResponderService = configurationResponderService;
        }

        [HttpGet("genders")]
        public async Task<ActionResult<StandardContainer<List<KeyValueVm>>>> GetGenders([FromQuery] string lang = "en")
        {
            return OkContainer(await Task.FromResult(configurationResponderService.GetGenders(lang)));
        }
    }
}
