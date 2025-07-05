using Users = Adapters.User.Domain.DTOs.User;

namespace Adapters.User.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Users?> GetByIdAsync(Guid id);
        Task<Users?> GetByEmailAsync(string email);
        Task<Users?> GetByEmailAndPasswordAsync(string email, string password);
        Task<IEnumerable<Users>> GetAllAsync();
        Task AddAsync(Users user);
        Task UpdateAsync(Users user);
        Task DeleteAsync(Guid id);
    }
}
