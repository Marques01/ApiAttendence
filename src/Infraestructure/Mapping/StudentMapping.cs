using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Mapping
{
    public class StudentMapping : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("tb_students");
            builder.HasKey(s => s.StudentId);

            builder.Property(s => s.Name).HasColumnType("varchar(100)");
            builder.Property(s => s.Registration).HasColumnType("varchar(20)");
        }
    }
}
