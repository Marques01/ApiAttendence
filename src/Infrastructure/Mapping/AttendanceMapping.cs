using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class AttendanceMapping : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.ToTable("tb_attendances");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.ScheduleId);
            builder.Property(a => a.StudentId);
            builder.Property(a => a.Date);
            builder.Property(a => a.Status).HasColumnType("varchar(50)");
            builder.Property(a => a.Notes).HasColumnType("varchar(500)");
            builder.Property(a => a.CreatedAt);
            builder.Property(a => a.UpdatedAt);

            // Relacionamentos
            builder.HasOne(a => a.Schedule)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.ScheduleId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Índices
            builder.HasIndex(a => a.ScheduleId);
            builder.HasIndex(a => a.StudentId);
            builder.HasIndex(a => a.Date);
            builder.HasIndex(a => a.Status);
            builder.HasIndex(a => new { a.ScheduleId, a.StudentId }).IsUnique();
        }
    }
}