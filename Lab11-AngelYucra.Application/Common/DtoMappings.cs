using Lab11_AngelYucra.Domain.DTOs;
using Lab11_AngelYucra.Domain.Models;

namespace Lab11_AngelYucra.Application.Common;

internal static class DtoMappings
{
    public static UserResponseDto ToDto(this User entity) =>
        new()
        {
            UserId = entity.UserId,
            Username = entity.Username,
            Email = entity.Email,
            CreatedAt = entity.CreatedAt
        };

    public static RoleResponseDto ToDto(this Role entity) =>
        new()
        {
            RoleId = entity.RoleId,
            RoleName = entity.RoleName
        };

    public static TicketResponseDto ToDto(this Ticket entity) =>
        new()
        {
            TicketId = entity.TicketId,
            UserId = entity.UserId,
            Title = entity.Title,
            Description = entity.Description,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            ClosedAt = entity.ClosedAt
        };

    public static ResponseResponseDto ToDto(this Response entity) =>
        new()
        {
            ResponseId = entity.ResponseId,
            TicketId = entity.TicketId,
            ResponderId = entity.ResponderId,
            Message = entity.Message,
            CreatedAt = entity.CreatedAt
        };

    public static UserRoleResponseDto ToDto(this UserRole entity) =>
        new()
        {
            UserId = entity.UserId,
            RoleId = entity.RoleId,
            AssignedAt = entity.AssignedAt,
            Username = entity.User?.Username,
            RoleName = entity.Role?.RoleName
        };
}
