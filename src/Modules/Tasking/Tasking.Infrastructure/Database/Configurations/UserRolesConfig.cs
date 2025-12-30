using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasking.Domain.ReadModels;

namespace Tasking.Infrastructure.Database.Configurations;

internal class UserRolesConfig : IEntityTypeConfiguration<TaskingUserRole>
{
    public void Configure(EntityTypeBuilder<TaskingUserRole> builder)
    {
        builder.ToTable("user_roles");
        builder.HasKey(ur => new { ur.UserId, ur.RoleName });
        builder.Property(ur => ur.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        builder.Property(ur => ur.RoleName)
            .HasColumnName("role_name")
            .IsRequired();
    }
}
