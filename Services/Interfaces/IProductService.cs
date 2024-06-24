using ShopApp.Models;

namespace ShopApp.Services.Interfaces;

public interface IProductService
{
    IEnumerable<Product> SearchProducts(IEnumerable<Product> products, string searchTerm);
    bool Buy(Guid productId, int quantityToBuy);
}
