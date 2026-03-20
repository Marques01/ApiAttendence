using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class RegisterLogMapping : IEntityTypeConfiguration<RegisterLog>
    {
        public void Configure(EntityTypeBuilder<RegisterLog> builder)
        {
            builder.ToTable("tb_register_logs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Message).HasColumnType("varchar(max)");
            builder.Property(x => x.Details).HasColumnType("varchar(max)");
            builder.Property(x => x.Origin).HasColumnType("varchar(max)");
            builder.Property(x => x.Exception).HasColumnType("varchar(max)");
            builder.Property(x => x.StackTrace).HasColumnType("varchar(max)");
            builder.Property(x => x.Inner).HasColumnType("varchar(max)");
        }
    }
}
