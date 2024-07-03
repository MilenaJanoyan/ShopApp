using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Services.Interfaces;
using static ShopApp.Enums.ProductEnums;

namespace ShopApp.Services;

public class ProductService : IProductService
{
    private readonly ShopDbContext _context;

    public ProductService(ShopDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> SearchProducts(IEnumerable<Product> products, string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return products;
        }

        var searchTermToLower = searchTerm.ToLower();
        return products.Where(product => product.Name.ToLower().Contains(searchTermToLower));
    }

    public bool Buy(Guid productId, int quantityToBuy)
    {
        var product = _context.Products.Find(productId);

        if (product == null)
        {
            return false;
        }

        if (product.StockQuantity < quantityToBuy)
        {
            // Not enough quantity in stock
            return false;
        }

        // Reduce the quantity in stock
        product.StockQuantity -= quantityToBuy;

        if (product.StockQuantity == 0)
        {
            product.Status = ProductStatus.OutOfStock;
        }

        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
