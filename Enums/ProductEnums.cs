namespace ShopApp.Enums;

public class ProductEnums
{
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

    public enum ProductStatus
    {
        InStock,
        OutOfStock,
        Discontinued
    }

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
