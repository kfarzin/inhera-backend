using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.VMs.Plan;

namespace Inhera.Shared.ModelMappers
{
    public static class PlanExtensions
    {
        public static PlanVm ToPlanVm(this PlanEntity entity)
        {
            return new PlanVm
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Description = entity.Description,
                FeaturesSummary = entity.FeaturesSummary,
                Currency = entity.Currency,
                PriceInCents = entity.PriceInCents,
                BillingCycle = entity.BillingCycle,
                ApplicableCountry = entity.ApplicableCountry,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive
            };
        }
    }
}
