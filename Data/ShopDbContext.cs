using Microsoft.EntityFrameworkCore;
using ShopApp.Users.Entity;
using ShopApp.Products.Entity;
using ShopApp.Authorization.Entity;

namespace ShopApp.Data;

public class ShopDbContext : DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Token> RefreshTokens { get; set; }
}
