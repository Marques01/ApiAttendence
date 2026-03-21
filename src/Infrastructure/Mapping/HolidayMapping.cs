using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class HolidayMapping : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.ToTable("tb_holidays");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name).HasColumnType("varchar(100)");
            builder.Property(h => h.Day);
            builder.Property(h => h.IsFixedDate).HasDefaultValue(false);
            builder.Property(h => h.National).HasDefaultValue(false);
            builder.Property(h => h.CreatedAt);
            builder.Property(h => h.UpdatedAt);

            // Índices
            builder.HasIndex(h => h.Day).IsUnique();
            builder.HasIndex(h => h.IsFixedDate);
            builder.HasIndex(h => h.National);
        }
    }
}