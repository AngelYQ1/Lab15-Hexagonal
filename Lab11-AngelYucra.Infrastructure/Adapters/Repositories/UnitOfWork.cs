using Lab11_AngelYucra.Domain.Models;
using Lab11_AngelYucra.Domain.Ports.IRepositories;

namespace Lab11_AngelYucra.Infrastructure.Adapters.Repositories;

public class UnitOfWork(TicketeraDbContext context) : IUnitOfWork
{
    private IGenericRepository<User>? _users;
    private IGenericRepository<Role>? _roles;
    private IGenericRepository<Ticket>? _tickets;
    private IGenericRepository<Response>? _responses;
    private IUserRoleRepository? _userRoles;

    public IGenericRepository<User> Users => _users ??= new GenericRepository<User>(context);
    public IGenericRepository<Role> Roles => _roles ??= new GenericRepository<Role>(context);
    public IGenericRepository<Ticket> Tickets => _tickets ??= new GenericRepository<Ticket>(context);
    public IGenericRepository<Response> Responses => _responses ??= new GenericRepository<Response>(context);
    public IUserRoleRepository UserRoles => _userRoles ??= new UserRoleRepository(context);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
