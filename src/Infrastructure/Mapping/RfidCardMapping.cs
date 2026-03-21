using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping
{
    public class RfidCardMapping : IEntityTypeConfiguration<RfidCard>
    {
        public void Configure(EntityTypeBuilder<RfidCard> builder)
        {
            builder.ToTable("tb_rfid_cards");
            builder.HasKey(r => r.RfidCardId);

            builder.Property(r => r.Code).HasColumnType("varchar(50)");

            // Relacionamento: Um RfidCard para muitos Students
            builder.HasMany(r => r.Students)
                .WithOne(s => s.RfidCard)
                .HasForeignKey(s => s.RfidCardId);
        }
    }
}