using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarkerCheckout.Infrastructure.Data
{
    public class SupermarketContextSeed
    {
        public static async Task SeedAsync(SupermarketContext supermarketContext)
        {
            if (!await supermarketContext.Skus.AnyAsync())
            {
                await supermarketContext.Skus.AddRangeAsync(GetPreconfiguredSkus());
            }
        }

        private static IEnumerable<Sku> GetPreconfiguredSkus()
        {
            var actualDate = DateTime.Now;

            var skuA = new Sku("A");
            skuA.AddSkuBasePrice(50);
            skuA.AddSkuOfferPrice(3, 43.3m, actualDate);

            var skuB = new Sku("B");
            skuB.AddSkuBasePrice(30);
            skuB.AddSkuOfferPrice(2, 22.5m, actualDate);

            var skuC = new Sku("C");
            skuC.AddSkuBasePrice(20);

            var skuD = new Sku("D");
            skuD.AddSkuBasePrice(15);

            return new List<Sku>() { skuA, skuB, skuC, skuD };
        }
    }
}
