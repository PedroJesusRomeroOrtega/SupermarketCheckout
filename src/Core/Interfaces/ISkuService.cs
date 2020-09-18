using SupermarketCheckout.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarketCheckout.Core.Interfaces
{
    public  interface ISkuService
    {
        Task<decimal> CalculatePrice(int skuId, int numberOfUnits);
        Task<decimal> CalculatePrice(IReadOnlyCollection<CheckoutUnit> units);
    }
}
