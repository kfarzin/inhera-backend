using Inhera.CoreAPI.Services;
using Inhera.Shared.ModelMappers;
using Inhera.Shared.Models.Common;
using Inhera.Shared.Services;
using Inhera.Shared.VMs.LabCenter.Vm;

namespace Inhera.CoreAPI.ResponderServices
{
    public class LabCenterResponderService : BaseResponderService
    {
        private readonly LabCenterService labCenterService;
        public LabCenterResponderService(
            LabCenterService labCenterService)
        {
            this.labCenterService = labCenterService;
        }

        public async Task<ServiceContainer<List<LabCenterVm>>> GetTestCenters(StandardPagination pagination, string? country)
        {
            try
            {
                var testCenters = await labCenterService.GetTestCenters(pagination, country);
                var result = testCenters.ToLabCenterVmList();
                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<List<LabCenterVm>>(null, LogException(ex));
            }
        }
    }
}
