using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tb_users");
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.Login).IsRequired().HasColumnType("varchar(100)");
            builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(100)");
            builder.Property(x => x.Password).HasColumnType("varchar(225)");
            builder.Property(x => x.Salt).HasColumnType("varchar(225)");
            builder.Property(u => u.CreateAt).HasPrecision(0);
            builder.Property(u => u.UpdateAt).HasPrecision(0);
            builder.Property(u => u.LastLogin).HasPrecision(0);

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }
    }
}
