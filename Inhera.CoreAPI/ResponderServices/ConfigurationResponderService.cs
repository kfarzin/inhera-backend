using Inhera.CoreAPI.Services;
using Inhera.Shared.Services;
using Inhera.Shared.VMs.Common;

namespace Inhera.CoreAPI.ResponderServices
{
    public class ConfigurationResponderService : BaseResponderService
    {
        private readonly ConfigurationService configurationService;

        public ConfigurationResponderService(ConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public ServiceContainer<List<KeyValueVm>> GetGenders(string lang = "en")
        {
            try
            {
                var genders = configurationService.GetGenders(lang);
                return OkContainer(genders);
            }
            catch (Exception ex)
            {
                return BadContainer<List<KeyValueVm>>(null, LogException(ex));
            }
        }
    }
}
