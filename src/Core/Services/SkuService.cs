using Ardalis.GuardClauses;
using SupermarketCheckout.Core.Entities;
using SupermarketCheckout.Core.Interfaces;
using SupermarketCheckout.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketCheckout.Core.Services
{
    public class SkuService : ISkuService
    {
        private readonly IAsyncRepository<Sku> _skuRepository;

        public SkuService(IAsyncRepository<Sku> skuRepository)
        {
            _skuRepository = skuRepository;
        }

        public async Task<decimal> CalculatePrice(DateTime date, int skuId, int numberOfUnits)
        {
            Guard.Against.OutOfSQLDateRange(date, nameof(date));
            Guard.Against.NegativeOrZero(numberOfUnits, nameof(numberOfUnits));

            var skuSpec = new SkuWithPricesSpecification(skuId);
            var sku = await _skuRepository.FirstAsync(skuSpec);
            var totalPrice = sku.CalculatePrice(date, numberOfUnits);
            return totalPrice;
        }

        public async Task<decimal> CalculatePrice(DateTime date, IEnumerable<(int skuId, int numberOfUnits)> units)
        {
            Guard.Against.OutOfSQLDateRange(date, nameof(date));

            var pricesTask = units.Select(async u => await CalculatePrice(date, u.skuId, u.numberOfUnits));
            var prices = await Task.WhenAll(pricesTask);
            return prices.Sum();
        }
    }
}