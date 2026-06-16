using Lab11_AngelYucra.Domain;
using Lab11_AngelYucra.Domain.Models;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lab11_AngelYucra.Infrastructure.Adapters.Repositories;

public class UserRoleRepository(TicketeraDbContext context) : IUserRoleRepository
{
    public async Task<IEnumerable<UserRole>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.UserRoles
            .Include(item => item.User)
            .Include(item => item.Role)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserRole?> GetByIdAsync(string userId, string roleId, CancellationToken cancellationToken = default)
    {
        return await context.UserRoles
            .Include(item => item.User)
            .Include(item => item.Role)
            .FirstOrDefaultAsync(item => item.UserId == userId && item.RoleId == roleId, cancellationToken);
    }

    public async Task AddAsync(UserRole entity, CancellationToken cancellationToken = default)
    {
        await context.UserRoles.AddAsync(entity, cancellationToken);
    }

    public void Delete(UserRole entity)
    {
        context.UserRoles.Remove(entity);
    }

    public async Task<bool> ExistsAsync(string userId, string roleId, CancellationToken cancellationToken = default)
    {
        return await context.UserRoles.AnyAsync(item => item.UserId == userId && item.RoleId == roleId, cancellationToken);
    }
}
