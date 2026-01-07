using Inhera.CoreAPI.Config;
using Inhera.CoreAPI.Controllers.Base;
using Inhera.CoreAPI.ResponderServices;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Util.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Inhera.CoreAPI.Controllers.LabCenterCalendar
{
    [LabCenterApiKeyAuth]
    [ApiController]
    public class LabCenterCalendarsController : CoreAPIController
    {
        private readonly LabCenterCalendarResponderService labCenterCalendarResponderService;
        private readonly ConsoleLogger logger;

        public LabCenterCalendarsController(
            LabCenterCalendarResponderService labCenterCalendarResponderService,
            ConsoleLogger consoleLogger)
        {
            this.labCenterCalendarResponderService = labCenterCalendarResponderService;
            this.logger = consoleLogger;
        }

        // Helper method to get authenticated lab center ID
        //protected Guid GetLabCenterId()
        //{
        //    return (Guid)HttpContext.Items["LabCenterId"]!;
        //}

        // API endpoints will be added here

        [HttpGet("me")]
        public ActionResult<LabCenterEntity> GetMyLabCenter(
            [AuthenticatedLabCenter] 
            LabCenterEntity labCenter)
        {
            return Ok(labCenter);
        }
    }
}
