using SupermarketCheckout.Core.Entities;
using SupermarketCheckout.Core.Interfaces;
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
            var sku = await _skuRepository.GetByIdAsync(skuId);
            var totalPrice = sku.CalculatePrice(numberOfUnits);
            return totalPrice;
        }
    }
}
