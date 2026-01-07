using Inhera.CoreAPI.Data;
using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Repositories;
using Inhera.Shared.Services;

namespace Inhera.CoreAPI.Services
{
    public class LabCenterCalendarService : SqlService<LabCenterCalendarEntity, CoreContext>
    {
        public LabCenterCalendarService(SqlRepository<LabCenterCalendarEntity, CoreContext> repository) : base(repository)
        {
        }

        // Service methods will be added here
    }
}
