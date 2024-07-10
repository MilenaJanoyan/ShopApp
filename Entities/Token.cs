namespace ShopApp.Entities;

public class Token
{
    public Guid Id { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpiryDate { get; set; }
}
