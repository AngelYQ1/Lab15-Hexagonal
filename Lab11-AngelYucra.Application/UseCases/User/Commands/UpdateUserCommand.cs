using Lab11_AngelYucra.Application.Common;
using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Ports.IRepositories;
using Lab11_AngelYucra.Domain.Ports.IServices;
using MediatR;

namespace Lab11_AngelYucra.Application.UseCases.User.Commands;

public class UpdateUserCommand : IRequest<UserResponseDto?>
{
    public string UserId { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string? Password { get; set; }
    public string? Email { get; set; }
}

internal sealed class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IAuthService authService) : IRequestHandler<UpdateUserCommand, UserResponseDto?>
{
    public async Task<UserResponseDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null)
            return null;

        var users = await unitOfWork.Users.GetAllAsync(cancellationToken);
        if (users.Any(item => item.UserId != request.UserId &&
                              item.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Username already exists.");

        if (!string.IsNullOrWhiteSpace(request.Email) &&
            users.Any(item => item.UserId != request.UserId &&
                              string.Equals(item.Email, request.Email, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Email already exists.");

        user.Username = request.Username.Trim();
        user.Email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim();

        if (!string.IsNullOrWhiteSpace(request.Password))
            user.PasswordHash = authService.HashPassword(request.Password);

        unitOfWork.Users.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return user.ToDto();
    }
}
