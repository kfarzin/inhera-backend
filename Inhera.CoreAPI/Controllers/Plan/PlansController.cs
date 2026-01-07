using Inhera.CoreAPI.Controllers.Base;
using Inhera.CoreAPI.ResponderServices;
using Inhera.Shared.Models.Common;
using Inhera.Shared.Util.Logging;
using Inhera.Shared.VMs.Plan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inhera.CoreAPI.Controllers.Plan
{
    [Authorize]
    [ApiController]
    public class PlansController : CoreAPIController
    {
        private readonly PlanResponderService planResponderService;
        private readonly ConsoleLogger logger;

        public PlansController(
            PlanResponderService planResponderService,
            ConsoleLogger consoleLogger)
        {
            logger = consoleLogger;
            this.planResponderService = planResponderService;
        }

        [HttpGet]
        public async Task<ActionResult<StandardContainer<List<PlanVm>>>> GetAllPlans([FromQuery] string? country = null)
        {
            return OkContainer(await planResponderService.GetAllPlans(country));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StandardContainer<PlanVm>>> GetPlanById(Guid id)
        {
            return OkContainer(await planResponderService.GetPlanById(id));
        }
    }
}
