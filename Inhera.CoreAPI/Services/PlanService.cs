using Inhera.CoreAPI.Data;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Repositories;
using Inhera.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Inhera.CoreAPI.Services
{
    public class PlanService : SqlService<PlanEntity, CoreContext>
    {
        public PlanService(SqlRepository<PlanEntity, CoreContext> repository) : base(repository)
        {
        }

        public async Task<List<PlanEntity>> GetAllPlans(string? country = null)
        {
            var query = _repository.GetRawRepository()
                .Where(e => e.IsActive);

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(e => e.ApplicableCountry == country);
            }

            var plans = await query.ToListAsync();

            return plans;
        }

        public async Task<PlanEntity?> GetPlanById(Guid id)
        {
            var plan = await _repository.GetRawRepository()
                .FirstOrDefaultAsync(e => e.Id == id);

            return plan;
        }
    }
}
