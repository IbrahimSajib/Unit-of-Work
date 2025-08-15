using Microsoft.EntityFrameworkCore;
using UnitOfWork.Core.Interfaces;
using UnitOfWork.Infrastructure.Data;

namespace UnitOfWork.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _db;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DataContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

    }

}
