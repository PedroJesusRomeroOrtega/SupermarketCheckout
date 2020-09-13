using Microsoft.EntityFrameworkCore;
using SupermarkerCheckout.Core.Entities;
using System.Reflection;

namespace SupermarkerCheckout.Infrastructure.Data
{
    public class SupermarketContext : DbContext
    {
        public SupermarketContext(DbContextOptions<SupermarketContext> options) : base(options)
        {
        }

        public DbSet<Sku> Skus { get; set; }
        public DbSet<SkuPrice> SkuPrices { get; set; }

        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CheckoutUnit> CheckoutUnits { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
