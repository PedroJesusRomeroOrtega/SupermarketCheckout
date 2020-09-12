using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SupermarkerCheckout.Infrastructure.Data.Config
{
    public class CheckoutUnitConfiguration : IEntityTypeConfiguration<CheckoutUnit>
    {
        public void Configure(EntityTypeBuilder<CheckoutUnit> builder)
        {
        }
    }
}
