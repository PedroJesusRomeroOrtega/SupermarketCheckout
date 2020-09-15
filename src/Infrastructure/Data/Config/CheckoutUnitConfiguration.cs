using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupermarketCheckout.Core.Entities;

namespace SupermarketCheckout.Infrastructure.Data.Config
{
    public class CheckoutUnitConfiguration : IEntityTypeConfiguration<CheckoutUnit>
    {
        public void Configure(EntityTypeBuilder<CheckoutUnit> builder)
        {
        }
    }
}
