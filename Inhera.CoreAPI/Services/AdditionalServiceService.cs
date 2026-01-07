using Inhera.CoreAPI.Data;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Repositories;
using Inhera.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Inhera.CoreAPI.Services
{
    public class AdditionalServiceService : SqlService<AdditionalServiceEntity, CoreContext>
    {
        public AdditionalServiceService(SqlRepository<AdditionalServiceEntity, CoreContext> repository) : base(repository)
        {
        }

        public async Task<List<AdditionalServiceEntity>> GetAllAdditionalServices(string? country = null)
        {
            var query = _repository.GetRawRepository()
                .Where(e => e.IsActive);

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(e => e.ApplicableCountry == country);
            }

            var services = await query.ToListAsync();

            return services;
        }

        public async Task<AdditionalServiceEntity?> GetAdditionalServiceById(Guid id)
        {
            var additionalService = await _repository.GetRawRepository()
                .FirstOrDefaultAsync(e => e.Id == id);

            return additionalService;
        }

        public async Task<List<AdditionalServiceEntity>> GetAdditionalServicesByIds(List<Guid> ids)
        {
            var additionalServices = await _repository.GetRawRepository()
                .Where(e => ids.Contains(e.Id))
                .ToListAsync();

            return additionalServices;
        }
    }
}
