using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tasking.Infrastructure.Database.Configurations;

internal class TaskConfig : IEntityTypeConfiguration<Domain.Aggregates.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Task> builder)
    {
        builder.ToTable("Tasks");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Title)
            .HasConversion(
                v => v.Value,
                static v => Domain.ValueObjects.TaskTitle.Create(v))
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasConversion(
                v => v.Value,
                static v => Domain.ValueObjects.TaskDescription.Create(v))
            .HasMaxLength(1000);

        builder.Property(t => t.DueDate)
            .HasConversion(
                v => v != null ? v.Value : (DateTime?)null,
                static v => v.HasValue ? Domain.ValueObjects.DueDate.Create(v.Value) : null);

        builder.OwnsMany(t => t.Items, items =>
        {
            items.ToTable("TaskItems");
            items.WithOwner().HasForeignKey(it => it.TaskId);
            items.Property<int>(it => it.Id)
                .ValueGeneratedOnAdd();
            items.HasKey(it => it.Id);
            items.Property(it => it.IsCompleted)
            .HasDefaultValue(false);
        });

        builder.OwnsMany(t => t.Comments, comments =>
        {
            comments.ToTable("TaskComments");
            comments.WithOwner().HasForeignKey(c => c.TaskId);
            comments.Property<int>(c => c.Id)
                .ValueGeneratedOnAdd();
            comments.HasKey(c => c.Id);
            comments.Property(c => c.Content)
                .HasMaxLength(1000);
            comments.Property(c => c.Author)
                .HasMaxLength(100);
        });

        // For Navigation Properties
        builder.Navigation(t => t.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(t => t.Comments)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
