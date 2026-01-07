using Inhera.CoreAPI.Services;
using Inhera.Shared.ModelMappers;
using Inhera.Shared.Services;
using Inhera.Shared.VMs.AdditionalService;

namespace Inhera.CoreAPI.ResponderServices
{
    public class AdditionalServiceResponderService : BaseResponderService
    {
        private readonly AdditionalServiceService additionalServiceService;

        public AdditionalServiceResponderService(AdditionalServiceService additionalServiceService)
        {
            this.additionalServiceService = additionalServiceService;
        }

        public async Task<ServiceContainer<List<AdditionalServiceVm>>> GetAllAdditionalServices(string? country = null)
        {
            try
            {
                var services = await additionalServiceService.GetAllAdditionalServices(country);
                var result = services.Select(s => s.ToAdditionalServiceVm()).ToList();
                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<List<AdditionalServiceVm>>(null, LogException(ex));
            }
        }

        public async Task<ServiceContainer<AdditionalServiceVm>> GetAdditionalServiceById(Guid id)
        {
            try
            {
                var service = await additionalServiceService.GetAdditionalServiceById(id);
                if (service == null)
                {
                    return BadContainer<AdditionalServiceVm>(null, "Additional service not found");
                }

                var result = service.ToAdditionalServiceVm();
                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<AdditionalServiceVm>(null, LogException(ex));
            }
        }
    }
}
