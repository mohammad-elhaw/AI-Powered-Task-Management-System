using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasking.Infrastructure.Projections;

namespace Tasking.Infrastructure.Database.Configurations.Projections;

internal class TaskingUserProjectionConfig : IEntityTypeConfiguration<TaskingUserProjection>
{
    public void Configure(EntityTypeBuilder<TaskingUserProjection> builder)
    {
        builder.ToTable("TaskingUsers");
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.UserId)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.DisplayName)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(x => x.Email)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(x => x.IsActive)
               .IsRequired();
    }
}
