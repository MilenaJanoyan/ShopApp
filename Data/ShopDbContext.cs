using ShopApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopApp.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
