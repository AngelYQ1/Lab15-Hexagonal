using Lab11_AngelYucra.Domain.Models;

namespace Lab11_AngelYucra.Domain.Ports.IRepositories;

public interface IUnitOfWork
{
    IGenericRepository<User> Users { get; }
    IGenericRepository<Role> Roles { get; }
    IGenericRepository<Ticket> Tickets { get; }
    IGenericRepository<Response> Responses { get; }
    IUserRoleRepository UserRoles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
