using Newtonsoft.Json.Linq;
using ShopApp.Entities;

namespace ShopApp.Repositories.Interfaces;

public interface ITokenRepository : IBaseRepository<Token>
{
    Task<Token> GetTokenByUserIdAsync(Guid userId);
    Task<Token> GetTokenByRefreshTokenAsync(string refreshToken);
    Task AddTokenAsync(Token token);
    Task UpdateTokenAsync(Token token);
    Task DeleteTokenAsync(Guid userId);
}
