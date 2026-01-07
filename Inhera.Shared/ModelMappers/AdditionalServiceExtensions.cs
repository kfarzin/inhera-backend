using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.VMs.AdditionalService;

namespace Inhera.Shared.ModelMappers
{
    public static class AdditionalServiceExtensions
    {
        public static AdditionalServiceVm ToAdditionalServiceVm(this AdditionalServiceEntity entity)
        {
            return new AdditionalServiceVm
            {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
                Description = entity.Description,
                FeaturesSummary = entity.FeaturesSummary,
                Currency = entity.Currency,
                PriceInCents = entity.PriceInCents,
                ApplicableCountry = entity.ApplicableCountry,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive
            };
        }
    }
}
