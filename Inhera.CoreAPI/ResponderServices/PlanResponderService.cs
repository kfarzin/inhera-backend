using Inhera.CoreAPI.Services;
using Inhera.Shared.ModelMappers;
using Inhera.Shared.Services;
using Inhera.Shared.VMs.Plan;

namespace Inhera.CoreAPI.ResponderServices
{
    public class PlanResponderService : BaseResponderService
    {
        private readonly PlanService planService;

        public PlanResponderService(PlanService planService)
        {
            this.planService = planService;
        }

        public async Task<ServiceContainer<List<PlanVm>>> GetAllPlans(string? country = null)
        {
            try
            {
                var plans = await planService.GetAllPlans(country);
                var result = plans.Select(p => p.ToPlanVm()).ToList();
                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<List<PlanVm>>(null, LogException(ex));
            }
        }

        public async Task<ServiceContainer<PlanVm>> GetPlanById(Guid id)
        {
            try
            {
                var plan = await planService.GetPlanById(id);
                if (plan == null)
                {
                    return BadContainer<PlanVm>(null, "Plan not found");
                }

                var result = plan.ToPlanVm();
                return OkContainer(result);
            }
            catch (Exception ex)
            {
                return BadContainer<PlanVm>(null, LogException(ex));
            }
        }
    }
}
