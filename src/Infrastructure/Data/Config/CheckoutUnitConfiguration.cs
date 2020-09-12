using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupermarkerCheckout.Core.Entities;

namespace SupermarkerCheckout.Infrastructure.Data.Config
{
    public class CheckoutUnitConfiguration : IEntityTypeConfiguration<CheckoutUnit>
    {
        public void Configure(EntityTypeBuilder<CheckoutUnit> builder)
        {
        }
    }
}
