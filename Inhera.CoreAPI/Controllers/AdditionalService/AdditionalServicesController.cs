using Inhera.CoreAPI.Controllers.Base;
using Inhera.CoreAPI.ResponderServices;
using Inhera.Shared.Models.Common;
using Inhera.Shared.Util.Logging;
using Inhera.Shared.VMs.AdditionalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inhera.CoreAPI.Controllers.AdditionalService
{
    [Authorize]
    [ApiController]
    public class AdditionalServicesController : CoreAPIController
    {
        private readonly AdditionalServiceResponderService additionalServiceResponderService;
        private readonly ConsoleLogger logger;

        public AdditionalServicesController(
            AdditionalServiceResponderService additionalServiceResponderService,
            ConsoleLogger consoleLogger)
        {
            logger = consoleLogger;
            this.additionalServiceResponderService = additionalServiceResponderService;
        }

        [HttpGet]
        public async Task<ActionResult<StandardContainer<List<AdditionalServiceVm>>>> GetAllAdditionalServices([FromQuery] string? country = null)
        {
            return OkContainer(await additionalServiceResponderService.GetAllAdditionalServices(country));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StandardContainer<AdditionalServiceVm>>> GetAdditionalServiceById(Guid id)
        {
            return OkContainer(await additionalServiceResponderService.GetAdditionalServiceById(id));
        }
    }
}
