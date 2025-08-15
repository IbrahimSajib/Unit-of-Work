using UnitOfWork.Core.Entities;
using UnitOfWork.Core.Interfaces;

namespace UnitOfWork.Services.Service
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _unitOfWork.Users.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id) => await _unitOfWork.Users.GetByIdAsync(id);

        public async Task<User> CreateAsync(User user)
        {
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user;
        }
        
        public async Task<bool> UpdateAsync(User user)
        {
            var existing = await _unitOfWork.Users.GetByIdAsync(user.Id);
            if (existing == null) return false;
            existing.Name = user.Name;
            existing.Email = user.Email;

            _unitOfWork.Users.Update(existing);
            var result = await _unitOfWork.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) return false;

            _unitOfWork.Users.Delete(user);
            var result = await _unitOfWork.SaveChangesAsync() > 0;
            return result;
        }

    }
}
