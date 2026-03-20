using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class RefreshTokenMapping : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("tb_refreshtoken");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Token).HasColumnType("varchar(max)");
            builder.Property(r => r.Expiration).HasColumnType("datetime");
            builder.Property(r => r.CreatAt).HasColumnType("datetime");
        }
    }
}
