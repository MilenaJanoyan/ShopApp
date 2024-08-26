using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Repositories;
using ShopApp.Authorization.Entity;
using ShopApp.Authorization.Repository.Interface;

namespace ShopApp.Authorization.Repository;

public class TokenRepository : BaseRepository<Token>, ITokenRepository
{
    public TokenRepository(ShopDbContext context) : base(context) { }

    public async Task<Token> GetTokenByUserIdAsync(Guid userId)
    {
        try
        {
            return await _context.Set<Token>().FirstOrDefaultAsync(t => t.UserId == userId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while fetching token by user ID: {ex.Message}", ex);
        }
    }


    public async Task<Token> GetTokenByRefreshTokenAsync(string refreshToken)
    {
        try
        {
            return await _context.Set<Token>().FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while fetching token by refresh token: {ex.Message}", ex);
        }
    }

    public async Task AddTokenAsync(Token token)
    {
        try
        {
            await base.AddAsync(token);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while adding token: {ex.Message}", ex);
        }
    }

    public async Task UpdateTokenAsync(Token token)
    {
        try
        {
            await base.UpdateAsync(token);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while updating token: {ex.Message}", ex);
        }
    }

    public async Task DeleteTokenAsync(Guid userId)
    {
        try
        {
            var token = await GetTokenByUserIdAsync(userId);
            if (token != null)
            {
                await base.DeleteAsync(token);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while deleting token: {ex.Message}", ex);
        }
    }
}
