using ShopApp.Data;
using ShopApp.Users.Entity;
using ShopApp.Repositories;
using ShopApp.Users.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ShopApp.Users.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ShopDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        {
            return await GetAllAsync();
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
            return await GetByIdAsync(userId);
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
            await AddAsync(user);
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
            await UpdateAsync(user);
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

    public async Task<User> GetUserByUsernameAsync(string userName)
    {
        try
        {
            var users = await GetAllAsync();

            var user = users.FirstOrDefault(u => u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                return null;
            }

            return user;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while retrieving the user: {ex.Message}", ex);
        }
    }

}
