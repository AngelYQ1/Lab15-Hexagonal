using Lab11_AngelYucra.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab11_AngelYucra.Infrastructure;

public class TicketeraDbContext(DbContextOptions<TicketeraDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Response> Responses => Set<Response>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(item => item.UserId);
            entity.Property(item => item.UserId).HasColumnName("user_id").HasMaxLength(36);
            entity.Property(item => item.Username).HasColumnName("username").HasMaxLength(100).IsRequired();
            entity.Property(item => item.PasswordHash).HasColumnName("password_hash").HasMaxLength(255).IsRequired();
            entity.Property(item => item.Email).HasColumnName("email").HasMaxLength(150);
            entity.Property(item => item.CreatedAt).HasColumnName("created_at");
            entity.HasIndex(item => item.Username).IsUnique();
            entity.HasIndex(item => item.Email).IsUnique();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");
            entity.HasKey(item => item.RoleId);
            entity.Property(item => item.RoleId).HasColumnName("role_id").HasMaxLength(36);
            entity.Property(item => item.RoleName).HasColumnName("role_name").HasMaxLength(50).IsRequired();
            entity.HasIndex(item => item.RoleName).IsUnique();
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("tickets");
            entity.HasKey(item => item.TicketId);
            entity.Property(item => item.TicketId).HasColumnName("ticket_id").HasMaxLength(36);
            entity.Property(item => item.UserId).HasColumnName("user_id").HasMaxLength(36).IsRequired();
            entity.Property(item => item.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
            entity.Property(item => item.Description).HasColumnName("description");
            entity.Property(item => item.Status)
                .HasColumnName("status")
                .HasColumnType("enum('abierto','en_proceso','cerrado')")
                .IsRequired();
            entity.Property(item => item.CreatedAt).HasColumnName("created_at");
            entity.Property(item => item.ClosedAt).HasColumnName("closed_at");
            entity.HasOne(item => item.User)
                .WithMany(user => user.Tickets)
                .HasForeignKey(item => item.UserId)
                .HasConstraintName("fk_tickets_user");
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.ToTable("responses");
            entity.HasKey(item => item.ResponseId);
            entity.Property(item => item.ResponseId).HasColumnName("response_id").HasMaxLength(36);
            entity.Property(item => item.TicketId).HasColumnName("ticket_id").HasMaxLength(36).IsRequired();
            entity.Property(item => item.ResponderId).HasColumnName("responder_id").HasMaxLength(36).IsRequired();
            entity.Property(item => item.Message).HasColumnName("message").IsRequired();
            entity.Property(item => item.CreatedAt).HasColumnName("created_at");
            entity.HasOne(item => item.Ticket)
                .WithMany(ticket => ticket.Responses)
                .HasForeignKey(item => item.TicketId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_responses_ticket");
            entity.HasOne(item => item.Responder)
                .WithMany(user => user.Responses)
                .HasForeignKey(item => item.ResponderId)
                .HasConstraintName("fk_responses_user");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("user_roles");
            entity.HasKey(item => new { item.UserId, item.RoleId });
            entity.Property(item => item.UserId).HasColumnName("user_id").HasMaxLength(36);
            entity.Property(item => item.RoleId).HasColumnName("role_id").HasMaxLength(36);
            entity.Property(item => item.AssignedAt).HasColumnName("assigned_at");
            entity.HasOne(item => item.User)
                .WithMany(user => user.UserRoles)
                .HasForeignKey(item => item.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_userroles_user");
            entity.HasOne(item => item.Role)
                .WithMany(role => role.UserRoles)
                .HasForeignKey(item => item.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_userroles_role");
        });
    }
}