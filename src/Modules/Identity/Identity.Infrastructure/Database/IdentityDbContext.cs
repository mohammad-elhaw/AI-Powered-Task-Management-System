using Identity.Domain.Aggregates;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Database;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) 
    : DbContext(options)
{

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(b =>
        {
            b.ToTable("Users");
            b.HasKey(u => u.Id);
            b.Property(u => u.Id).ValueGeneratedNever();
            b.Property(u => u.KeycloakId).IsRequired();

            b.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value).HasColumnName("Email").IsRequired().HasMaxLength(256);
            });

            b.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired().HasMaxLength(50);
                name.Property(n => n.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(50);
            });

            b.Property(u => u.IsActive).HasDefaultValue(true);

            b.HasMany(u => u.UserRoles)
             .WithOne(ur => ur.User)
             .HasForeignKey(ur => ur.UserId);
        });

        // Role
        modelBuilder.Entity<Role>(b =>
        {
            b.ToTable("Roles");
            b.HasKey(r => r.Id);
            b.Property(r => r.Id).ValueGeneratedNever();
            b.Property(r => r.Name).HasMaxLength(100).IsRequired();

            b.HasMany(r => r.Permissions)
             .WithOne()
             .HasForeignKey("RoleId")
             .OnDelete(DeleteBehavior.Cascade);
        });

        // Permission
        modelBuilder.Entity<Permission>(b =>
        {
            b.ToTable("Permissions");
            b.HasKey(p => p.Id);
            b.Property(p => p.Name).HasMaxLength(200).IsRequired();
        });

        // UserRole (join table)
        modelBuilder.Entity<UserRole>(b =>
        {
            b.ToTable("UserRoles");
            b.HasKey(ur => new { ur.UserId, ur.RoleId });

            b.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
