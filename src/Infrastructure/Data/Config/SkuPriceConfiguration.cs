using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupermarketCheckout.Core.Entities;

namespace SupermarketCheckout.Infrastructure.Data.Config
{
    public class SkuPriceConfiguration : IEntityTypeConfiguration<SkuPrice>
    {
        public void Configure(EntityTypeBuilder<SkuPrice> builder)
        {
            builder.Property(sp => sp.PricePerUnit)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");
        }
    }
}
