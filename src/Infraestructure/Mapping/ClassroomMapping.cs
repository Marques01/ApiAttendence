using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class ClassroomMapping : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.ToTable("tb_classrooms");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasColumnType("varchar(100)");
            builder.Property(c => c.Capacity);
            builder.Property(c => c.Location).HasColumnType("varchar(100)");
            builder.Property(c => c.IsAvailable).HasDefaultValue(true);
            builder.Property(c => c.CreatedAt);
            builder.Property(c => c.UpdatedAt);

            // Relacionamento
            builder.HasMany(c => c.Classes)
                .WithOne(cl => cl.Classroom)
                .HasForeignKey(cl => cl.ClassroomId)
                .OnDelete(DeleteBehavior.NoAction);

            // Índices
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}