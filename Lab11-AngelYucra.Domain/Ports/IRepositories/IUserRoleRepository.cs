using Lab11_AngelYucra.Domain.Models;

namespace Lab11_AngelYucra.Domain.Ports.IRepositories;

public interface IUserRoleRepository
{
    Task<IEnumerable<UserRole>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<UserRole?> GetByIdAsync(string userId, string roleId, CancellationToken cancellationToken = default);
    Task AddAsync(UserRole entity, CancellationToken cancellationToken = default);
    void Delete(UserRole entity);
    Task<bool> ExistsAsync(string userId, string roleId, CancellationToken cancellationToken = default);
}
