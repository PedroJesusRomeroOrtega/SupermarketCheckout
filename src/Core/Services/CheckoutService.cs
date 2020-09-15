using SupermarketCheckout.Core.Entities;
using SupermarketCheckout.Core.Interfaces;
using SupermarketCheckout.Core.Specifications;
using System.Threading.Tasks;

namespace SupermarketCheckout.Core.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IAsyncRepository<Checkout> _checkoutRepository;
        public CheckoutService(IAsyncRepository<Checkout> checkoutRespository)
        {
            _checkoutRepository = checkoutRespository;
        }

        public async Task<Checkout> GetOrCreateCheckout(int? checkoutId)
        {
            if (!checkoutId.HasValue)
            {
                return await _checkoutRepository.AddAsync(new Checkout());
            }
            var checkoutSpecification = new CheckoutWithUnitsSpecification(checkoutId.Value);
            var checkout = await _checkoutRepository.FirstAsync(checkoutSpecification);
            //TODO: add guard if checkout is null.
            return checkout;
        }

        public async Task<int> AddUnits(Checkout checkout, int skuId, int numberOfUnits = 1)
        {
            var totalUnits = checkout.AddUnit(skuId, numberOfUnits);
            await _checkoutRepository.UpdateAsync(checkout);
            return totalUnits;
        }
    }
}
