using Ardalis.GuardClauses;
using SupermarketCheckout.Core.Entities;
using SupermarketCheckout.Core.Interfaces;
using SupermarketCheckout.Core.Specifications;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Checkout>> GetCheckouts()
        {
            var checkoutSpecification = new CheckoutWithUnitsSpecification();
            return await _checkoutRepository.ListAsync(checkoutSpecification);
        }

        public async Task<Checkout> GetOrCreateCheckout(int? checkoutId)
        {
            if (!checkoutId.HasValue)
            {
                return await _checkoutRepository.AddAsync(new Checkout());
            }
            var checkoutSpecification = new CheckoutWithUnitsSpecification(checkoutId.Value);
            var checkout = await _checkoutRepository.FirstAsync(checkoutSpecification);
            Guard.Against.Null(checkout, nameof(checkout));
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
