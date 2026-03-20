using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class HabilitationMapping : IEntityTypeConfiguration<Habilitation>
    {
        public void Configure(EntityTypeBuilder<Habilitation> builder)
        {
            builder.ToTable("tb_habilitaions");
            builder.HasKey(h => h.HabilitationId);

            builder.Property(h => h.Name).HasColumnType("varchar(100)");
            builder.Property(h => h.Description).HasColumnType("varchar(500)");
            builder.Property(h => h.Enabled).HasDefaultValue(true);
            builder.Property(h => h.CreatedAt);
            builder.Property(h => h.UpdatedAt);
            builder.Property(h => h.DisabledAt);

            // Relacionamento com TeacherHabilitation
            builder.HasMany(h => h.TeacherHabilitations)
                .WithOne(th => th.Habilitation)
                .HasForeignKey(th => th.HabilitationId);
        }
    }
}