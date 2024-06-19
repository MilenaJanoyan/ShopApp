using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Services.Interfaces;

namespace ShopApp.Services
{
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
    }
}
