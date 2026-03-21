using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class ScheduleMapping : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("tb_schedules");
            builder.HasKey(s => s.ScheduleId);

            builder.Property(s => s.ClassId);
            builder.Property(s => s.TeacherId);
            builder.Property(s => s.Date);
            builder.Property(s => s.DayOfWeek);
            builder.Property(s => s.StartTime);
            builder.Property(s => s.EndTime);
            builder.Property(s => s.IsHoliday).HasDefaultValue(false);
            builder.Property(s => s.Enabled).HasDefaultValue(true);
            builder.Property(s => s.CreatedAt);
            builder.Property(s => s.UpdatedAt);
            builder.Property(s => s.DisabledAt);

            // Relacionamentos
            builder.HasOne(s => s.Classes)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.ClassId);

            builder.HasOne(s => s.Teacher)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.TeacherId);

            builder.HasMany(s => s.Attendances)
                .WithOne(a => a.Schedule)
                .HasForeignKey(a => a.ScheduleId);
        }
    }
}