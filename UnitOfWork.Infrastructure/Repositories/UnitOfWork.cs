using UnitOfWork.Core.Entities;
using UnitOfWork.Core.Interfaces;
using UnitOfWork.Infrastructure.Data;

namespace UnitOfWork.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _db;
        private IGenericRepository<User>? _userRepository;

        public UnitOfWork(DataContext db)
        {
            _db = db;
        }

        public IGenericRepository<User> Users => _userRepository ??= new GenericRepository<User>(_db);

        public async Task<int> SaveChangesAsync() => await _db.SaveChangesAsync();

        public void Dispose() => _db.Dispose();

    }
}
