using Inhera.CoreAPI.Services;
using Inhera.Shared.Services;

namespace Inhera.CoreAPI.ResponderServices
{
    public class LabCenterCalendarResponderService : BaseResponderService
    {
        private readonly LabCenterCalendarService labCenterCalendarService;

        public LabCenterCalendarResponderService(LabCenterCalendarService labCenterCalendarService)
        {
            this.labCenterCalendarService = labCenterCalendarService;
        }

        // Business logic methods will be added here
    }
}
