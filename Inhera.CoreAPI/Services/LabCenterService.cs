using Inhera.CoreAPI.Data;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Models.Common;
using Inhera.Shared.Repositories;
using Inhera.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Inhera.CoreAPI.Services
{
    public class LabCenterService : SqlService<LabCenterEntity, CoreContext>
    {
        public LabCenterService(SqlRepository<LabCenterEntity, CoreContext> repository) : base(repository)
        {
        }

        public async Task<List<LabCenterEntity>> GetTestCenters(
            StandardPagination pagination,
            string? country)
        {
            var labCenters = await _repository
                 .GetRawRepository()                 
                 .AsQueryable()
                 .Where(e => e.Address != null)
                 .Where(e => e.Address!.Country == country)
                 .Skip(pagination.GetSkips())
                 .Take(pagination.GetTakes())
                 .Include(e => e.Address)
                 .ToListAsync();

            return labCenters;
        }
    }
}