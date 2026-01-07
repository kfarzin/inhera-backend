using Inhera.CoreAPI.Controllers.Base;
using Inhera.CoreAPI.ResponderServices;
using Inhera.Shared.Models.Common;
using Inhera.Shared.VMs.Profile.Vm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inhera.CoreAPI.Controllers.TestCenter
{
    [Authorize]
    [ApiController]
    public class TestCentersController : CoreAPIController
    {
        private readonly LabCenterResponderService labCenterResponderService;

        public TestCentersController(
            LabCenterResponderService labCenterResponderService
            )
        {
            this.labCenterResponderService = labCenterResponderService;
        }

        [HttpGet]
        public async Task<ActionResult<StandardContainer<UserProfileVm>>> GetTestCenters(
            [FromQuery]
            StandardPagination pagination,
            [FromQuery]
            string? country = "de"
            )
        {
            return CreatedContainer(await labCenterResponderService.GetTestCenters(pagination, country));
        }
    }
}