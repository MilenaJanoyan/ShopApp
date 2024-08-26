using ShopApp.Products.Enum;

namespace ShopApp.Products.Entity;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductEnums.ProductCategory Category { get; set; }
    public ProductEnums.ProductStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<string> Tags { get; set; }
    public int StockQuantity { get; set; }
    public ProductEnums.ProductUnit Unit { get; set; }
}
