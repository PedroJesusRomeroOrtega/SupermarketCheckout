using SupermarketCheckout.Core.Entities;
using SupermarketCheckout.Core.Interfaces;
using SupermarketCheckout.Core.Specifications;
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

        public async Task<decimal> CalculatePrice(int skuId, int numberOfUnits)
        {
            var skuSpec = new SkuWithPricesSpecification(skuId);
            var sku = await _skuRepository.FirstAsync(skuSpec);
            var totalPrice = sku.CalculatePrice(numberOfUnits);
            return totalPrice;
        }

        //TODO: make test
        public async Task<decimal> CalculatePrice(IReadOnlyCollection<CheckoutUnit> units)
        {
            var pricesTask = units.Select(async u => await CalculatePrice(u.SkuId, u.NumberOfUnits));
            var prices = await Task.WhenAll(pricesTask);
            return prices.Sum();
        }
    }
}
