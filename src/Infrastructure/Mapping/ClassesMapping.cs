using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class ClassesMapping : IEntityTypeConfiguration<Classes>
    {
        public void Configure(EntityTypeBuilder<Classes> builder)
        {
            builder.ToTable("tb_classes");
            builder.HasKey(c => c.ClassId);

            builder.Property(c => c.Name).HasColumnType("varchar(100)");
            builder.Property(c => c.TeacherId);
            builder.Property(c => c.ClassroomId);
            builder.Property(c => c.StartDate);
            builder.Property(c => c.EndDate);
            builder.Property(c => c.Notes).HasColumnType("varchar(500)");
            builder.Property(c => c.Enabled).HasDefaultValue(true);
            builder.Property(c => c.CreatedAt);
            builder.Property(c => c.UpdatedAt);

            // Relacionamentos
            builder.HasOne(c => c.Teacher)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Classroom)
                .WithMany(cr => cr.Classes)
                .HasForeignKey(c => c.ClassroomId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.Schedules)
                .WithOne(s => s.Classes)
                .HasForeignKey(s => s.ClassId)
                .OnDelete(DeleteBehavior.NoAction);

            // Índices
            builder.HasIndex(c => c.TeacherId);
            builder.HasIndex(c => c.ClassroomId);
            builder.HasIndex(c => new { c.StartDate, c.EndDate });
        }
    }
}