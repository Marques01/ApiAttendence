using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class TeacherMapping : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("tb_teachers");
            builder.HasKey(t => t.TeacherId);

            builder.Property(t => t.Name).HasColumnType("varchar(100)");
            builder.Property(t => t.Registration).HasColumnType("varchar(20)");
            builder.Property(t => t.Email).HasColumnType("varchar(255)");
            builder.Property(t => t.Enabled).HasDefaultValue(true);
            builder.Property(t => t.CreatedAt);
            builder.Property(t => t.UpdatedAt);
            builder.Property(t => t.DisabledAt);            
        }
    }
}