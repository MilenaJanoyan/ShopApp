using ShopApp.Data;
using ShopApp.Entities;
using ShopApp.Repositories.Interfaces;

namespace ShopApp.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ShopDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        {
            return await base.GetAllAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while fetching users: {ex.Message}", ex);
        }
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        try
        {
            return await base.GetByIdAsync(userId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while fetching user by ID: {ex.Message}", ex);
        }
    }

    public async Task AddUserAsync(User user)
    {
        try
        {
            await base.AddAsync(user);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while adding user: {ex.Message}", ex);
        }
    }

    public async Task UpdateUserAsync(User user)
    {
        try
        {
            await base.UpdateAsync(user);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while updating user: {ex.Message}", ex);
        }
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        try
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            await base.DeleteAsync(user);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while deleting user: {ex.Message}", ex);
        }
    }
}
