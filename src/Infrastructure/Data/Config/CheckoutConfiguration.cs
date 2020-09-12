using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupermarkerCheckout.Core.Entities;

namespace SupermarkerCheckout.Infrastructure.Data.Config
{
    public class CheckoutConfiguration : IEntityTypeConfiguration<Checkout>
    {
        public void Configure(EntityTypeBuilder<Checkout> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Checkout.Units));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
