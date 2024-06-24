using ShopApp.Models;

namespace ShopApp.Repositories.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(Guid productId);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid productId);
}
