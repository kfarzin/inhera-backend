

using Inhera.Shared.Database.SQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Inhera.Shared.Repositories
{
    public class SqlRepository<T, K> : ISqlRepository<T> where T : SqlEntity where K : DbContext
    {
        private readonly DbSet<T> _repository;
        private readonly K _context;
        public SqlRepository(K context)
        {
            _context = context;
            _repository = _context.Set<T>();
        }

        public IDbContextTransaction GetTransaction()
        {
            return GetConnection().BeginTransaction();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public DatabaseFacade GetConnection()
        {
            return _context.Database;
        }

        public DbSet<T> GetRawRepository()
        {
            return _repository;
        }

        public K GetContext()
        {
            return _context;
        }

        public async Task<T> Update(T model, bool saveChanges = true)
        {
            _context.ChangeTracker.Clear();
            SetUpdatedAt(model);
            _repository.Update(model);
            if (saveChanges)
            {
                await _context.SaveChangesAsync();
            }
            return model;
        }

        public async Task<T> Add(T model, bool saveChanges = true)
        {            
            SetCreatedAndUpdatedAt(model);
            await _repository.AddAsync(model);
            if (saveChanges)
            {
                await _context.SaveChangesAsync();                
            }
            return model;
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetById(string id)
        {
            var model = await _repository.FirstOrDefaultAsync(e => e.Id.ToString() == id);
            return model;
        }

        private void SetCreatedAt(T model)
        {
            model.CreatedAt = DateTimeOffset.UtcNow;
        }

        private void SetUpdatedAt(T model)
        {
            model.UpdatedAt = DateTimeOffset.UtcNow;
        }

        private void SetCreatedAndUpdatedAt(T model)
        {
            model.CreatedAt = DateTimeOffset.UtcNow;
            model.UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
