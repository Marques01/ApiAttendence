using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class RolesMapping : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("tb_roles");
            builder.HasKey(x => x.RoleId);
            builder.Property(x => x.Name).HasColumnType("varchar(50)");
        }
    }
}
