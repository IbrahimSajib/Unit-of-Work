using UnitOfWork.Core.Entities;

namespace UnitOfWork.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> Users { get; }
        Task<int> SaveChangesAsync();
    }
}
