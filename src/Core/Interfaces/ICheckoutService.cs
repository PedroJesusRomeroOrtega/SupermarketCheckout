using SupermarketCheckout.Core.Entities;
using System.Threading.Tasks;

namespace SupermarketCheckout.Core.Interfaces
{
    public interface ICheckoutService
    {
        Task<Checkout> GetOrCreateCheckout(int? checkoutId);
        Task AddUnits(Checkout checkout, int skuId, int numberOfUnits = 1);
    }
}
