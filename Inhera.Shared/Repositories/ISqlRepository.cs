using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Inhera.Shared.Database.SQL.Entities;

namespace Inhera.Shared.Repositories
{
    public interface ISqlRepository<T> where T : SqlEntity
    {
        IDbContextTransaction GetTransaction();
        DatabaseFacade GetConnection();        
        Task<T?> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T model, bool saveChanges);
    }
}
