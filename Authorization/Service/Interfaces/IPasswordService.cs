namespace ShopApp.Authorization.Service.Interfaces;

public interface IPasswordService
{
    string GenerateSalt();
    string HashPassword(string password, string salt);
    bool VerifyPassword(string password, string hashedPassword, string salt);
}
