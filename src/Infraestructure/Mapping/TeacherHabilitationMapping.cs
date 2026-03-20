using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class TeacherHabilitationMapping : IEntityTypeConfiguration<TeacherHabilitation>
    {
        public void Configure(EntityTypeBuilder<TeacherHabilitation> builder)
        {
            builder.ToTable("tb_teacher_habilitaions");
            builder.HasKey(th => th.TeacherHabilitationId);

            builder.Property(th => th.Enabled).HasDefaultValue(true);
            builder.Property(th => th.CreatedAt);
            builder.Property(th => th.UpdatedAt);
            builder.Property(th => th.DisabledAt);

            // Relacionamentos
            builder.HasOne(th => th.Teacher)
                .WithMany(t => t.TeacherHabilitations)
                .HasForeignKey(th => th.TeacherId);

            builder.HasOne(th => th.Habilitation)
                .WithMany(h => h.TeacherHabilitations)
                .HasForeignKey(th => th.HabilitationId);
        }
    }
}