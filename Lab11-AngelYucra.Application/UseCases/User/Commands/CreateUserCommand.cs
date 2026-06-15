using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using Lab11_AngelYucra.Domain.Ports.IServices;
using MediatR;
using UserEntity = Lab11_AngelYucra.Domain.Models.User;

namespace Lab11_AngelYucra.Application.UseCases.User.Commands;

public class CreateUserCommand : IRequest<UserResponseDto>
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? Email { get; set; }
}

internal sealed class CreateUserCommandHandler(IUnitOfWork unitOfWork, IAuthService authService) : IRequestHandler<CreateUserCommand, UserResponseDto>
{
    public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Username and password are required.");

        var users = await unitOfWork.Users.GetAllAsync(cancellationToken);
        if (users.Any(user => user.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Username already exists.");

        if (!string.IsNullOrWhiteSpace(request.Email) &&
            users.Any(user => string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Email already exists.");

        var entity = new UserEntity
        {
            UserId = Guid.NewGuid().ToString(),
            Username = request.Username.Trim(),
            PasswordHash = authService.HashPassword(request.Password),
            Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await unitOfWork.Users.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.ToDto();
    }
}
