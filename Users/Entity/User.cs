namespace ShopApp.Users.Entity;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Salt { get; set; }
}
