using Xunit;
using ShopApp.Data;
using ShopApp.Models;
using ShopApp.Services;
using ShopApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static ShopApp.Enums.ProductEnums;

namespace ShopApp.Tests;

public class ProductServiceTests
{
    private readonly ShopDbContext _context;
    private readonly IProductService _productService;

    public ProductServiceTests()
    {
        var options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_Products_Database")
            .Options;

        _context = new ShopDbContext(options);
        SeedDatabase();

        _productService = new ProductService(_context);
    }

    private void SeedDatabase()
    {
        _context.Products.AddRange(
            new Product { Id = Guid.NewGuid(), Name = "Product1", StockQuantity = 10, Status = ProductStatus.InStock, Tags = new List<string> { "tag1", "tag2" } },
            new Product { Id = Guid.NewGuid(), Name = "TestProduct2", StockQuantity = 5, Status = ProductStatus.InStock, Tags = new List<string> { "tag3", "tag4" } },
            new Product { Id = Guid.NewGuid(), Name = "Product3", StockQuantity = 0, Status = ProductStatus.OutOfStock, Tags = new List<string>() }
        );
        _context.SaveChanges();
    }

    [Fact]
    public void SearchProducts_WithNullOrEmptySearchTerm_ReturnsAllProducts()
    {
        var products = _context.Products.ToList();

        var result = _productService.SearchProducts(products, string.Empty);

        Assert.Equal(products, result);
    }

    [Fact]
    public void SearchProducts_WithSearchTerm_ReturnsFilteredProducts()
    {
        var products = _context.Products.ToList();

        var result = _productService.SearchProducts(products, "test");

        Assert.Single(result);
        Assert.Contains(products.First(p => p.Name == "TestProduct2"), result);
    }

    [Fact]
    public void Buy_ProductDoesNotExist_ReturnsFalse()
    {
        var productId = Guid.NewGuid();

        var result = _productService.Buy(productId, 1);

        Assert.False(result);
    }

    [Fact]
    public void Buy_NotEnoughStock_ReturnsFalse()
    {
        var productId = _context.Products.First(p => p.Name == "Product1").Id;

        var result = _productService.Buy(productId, 20);

        Assert.False(result);
    }

    [Fact]
    public void Buy_SufficientStock_ReducesStockAndReturnsTrue()
    {
        var productId = _context.Products.First(p => p.Name == "Product1").Id;

        var result = _productService.Buy(productId, 2);

        var product = _context.Products.Find(productId);
        Assert.True(result);
        Assert.Equal(8, product.StockQuantity);
    }

    [Fact]
    public void Buy_ReducesStockToZero_SetsStatusToOutOfStock()
    {
        var productId = _context.Products.First(p => p.Name == "TestProduct2").Id;

        var result = _productService.Buy(productId, 5);

        var product = _context.Products.Find(productId);
        Assert.True(result);
        Assert.Equal(0, product.StockQuantity);
        Assert.Equal(ProductStatus.OutOfStock, product.Status);
    }
}
