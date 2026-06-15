namespace Lab11_AngelYucra.Domain.Models;

public class Role
{
    public string RoleId { get; set; } = default!;
    public string RoleName { get; set; } = default!;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
