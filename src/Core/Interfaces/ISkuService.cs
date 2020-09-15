using System.Threading.Tasks;

namespace SupermarketCheckout.Core.Interfaces
{
    public  interface ISkuService
    {
        Task<decimal> CalculatePrice(int skuId, int numberOfUnits);
    }
}
