using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class UserRolesMapping : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.ToTable("tb_user_roles");
            builder.HasKey(x => x.UserRolesId);
        }
    }
}
