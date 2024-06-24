using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Repositories.Interfaces;

namespace ShopApp.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ShopDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        try
        {
            return await base.GetAllAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while fetching products: {ex.Message}", ex);
        }
    }

    public async Task<Product> GetProductByIdAsync(Guid productId)
    {
        try
        {
            return await base.GetByIdAsync(productId);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while fetching product by ID: {ex.Message}", ex);
        }
    }

    public async Task AddProductAsync(Product product)
    {
        try
        {
            await base.AddAsync(product);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while adding product: {ex.Message}", ex);
        }
    }

    public async Task UpdateProductAsync(Product product)
    {
        try
        {
            await base.UpdateAsync(product);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while updating product: {ex.Message}", ex);
        }
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        try
        {
            var product = await GetByIdAsync(productId);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            await base.DeleteAsync(product);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error occurred while deleting product: {ex.Message}", ex);
        }
    }
}
