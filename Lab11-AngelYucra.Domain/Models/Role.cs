namespace Lab11_AngelYucra.Domain.Models;

public class Role
{
    public string RoleId { get; set; } = null!;
    public string RoleName { get; set; } = null!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}