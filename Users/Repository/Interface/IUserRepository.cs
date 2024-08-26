using ShopApp.Repositories.Interfaces;
using ShopApp.Users.Entity;

namespace ShopApp.Users.Repository.Interface;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid userId);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid userId);
    Task<User> GetUserByUsernameAsync(string userName);
}
