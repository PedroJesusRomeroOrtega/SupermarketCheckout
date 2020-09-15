using Ardalis.Specification;
using SupermarketCheckout.Core.Entities;

namespace SupermarketCheckout.Core.Specifications
{
    public sealed class SkuWithPricesSpecification : Specification<Sku>
    {
        public SkuWithPricesSpecification(int skuId)
        {
            Query
                .Where(s => s.Id == skuId)
                .Include(s => s.SkuPrices);
        }
    }
}
