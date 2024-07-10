using ShopApp.Entities;

namespace ShopApp.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid userId);
}
