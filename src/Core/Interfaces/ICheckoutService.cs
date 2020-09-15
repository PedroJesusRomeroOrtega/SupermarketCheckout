using SupermarketCheckout.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarketCheckout.Core.Interfaces
{
    public interface ICheckoutService
    {
        Task<IEnumerable<Checkout>> GetCheckouts();
        Task<Checkout> GetOrCreateCheckout(int? checkoutId);
        Task<int> AddUnits(Checkout checkout, int skuId, int numberOfUnits = 1);
    }
}
