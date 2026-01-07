

using Inhera.Shared.Database.SQL.Entities;
using Inhera.Shared.Models.Enums;
using Inhera.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inhera.Shared.Services
{
    public class SqlBaseService<T, K> : IService where T : SqlEntity where K : DbContext
    {
        protected readonly SqlRepository<T, K> _repository;

        public string ValidationErrorOf(ValidationErrors error)
        {
            return error.ToString();
        }
        public SqlBaseService(SqlRepository<T, K> repository)
        {
            _repository = repository;
        }
    }
}
