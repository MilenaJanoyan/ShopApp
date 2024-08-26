using ShopApp.Products.Entity;

namespace ShopApp.Products.Service.Interface;

public interface IProductService
{
    IEnumerable<Product> SearchProducts(IEnumerable<Product> products, string searchTerm);
    bool Buy(Guid productId, int quantityToBuy);
}
