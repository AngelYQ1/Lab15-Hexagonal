using Lab11_AngelYucra.Domain.Models;

namespace Lab11_AngelYucra.Domain.Ports.IServices;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
    string GenerateToken(User user, IEnumerable<string> roles);
}
