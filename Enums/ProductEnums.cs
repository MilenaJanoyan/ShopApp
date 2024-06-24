namespace ShopApp.Enums;

public class ProductEnums
{
    // Enum for product unit
    public enum ProductUnit
    {
        Piece,
        Pack,
        Dozen,
        Gram,
        Kilogram,
        Liter,
        Milliliter
    }

    // Enum for product status
    public enum ProductStatus
    {
        InStock,
        OutOfStock,
        Discontinued
    }

    // Enum for product categories
    public enum ProductCategory
    {
        Electronics,
        Books,
        Clothing,
        Home,
        Beauty,
        Food,
        Sports
    }
}
