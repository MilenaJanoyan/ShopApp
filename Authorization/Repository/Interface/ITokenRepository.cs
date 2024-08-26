using ShopApp.Authorization.Entity;
using ShopApp.Repositories.Interfaces;

namespace ShopApp.Authorization.Repository.Interface;

public interface ITokenRepository : IBaseRepository<Token>
{
    Task<Token> GetTokenByUserIdAsync(Guid userId);
    Task<Token> GetTokenByRefreshTokenAsync(string refreshToken);
    Task AddTokenAsync(Token token);
    Task UpdateTokenAsync(Token token);
    Task DeleteTokenAsync(Guid userId);
}
