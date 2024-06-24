using static ShopApp.Enums.ProductEnums;

namespace ShopApp.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductCategory Category { get; set; }
    public ProductStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<string> Tags { get; set; }
    public int StockQuantity { get; set; }
    public ProductUnit Unit { get; set; }
}
