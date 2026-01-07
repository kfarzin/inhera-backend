using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inhera.Shared.Services
{
    public class SqlService<T, K> : SqlBaseService<T, K> where T : SqlEntity where K : DbContext
    {
        public SqlService(SqlRepository<T, K> repository) : base(repository)
        {
        }

        protected TK AddTimeStamps<TK>(TK model) where TK : SqlEntity
        {
            model.CreatedAt = DateTimeOffset.UtcNow;
            model.UpdatedAt = DateTimeOffset.UtcNow;
            return model;
        }

        protected TK AddUpdateTimeStamp<TK>(TK model) where TK : SqlEntity
        {            
            model.UpdatedAt = DateTimeOffset.UtcNow;
            return model;
        }
    }
}