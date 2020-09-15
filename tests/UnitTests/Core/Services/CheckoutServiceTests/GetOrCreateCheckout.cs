using Moq;
using SupermarketCheckout.Core.Entities;
using SupermarketCheckout.Core.Interfaces;
using SupermarketCheckout.Core.Services;
using SupermarketCheckout.Core.Specifications;
using System.Threading.Tasks;
using Xunit;

namespace SupermarketCheckout.UnitTests.Core.Services.CheckoutServiceTests
{
    public class GetOrCreateCheckout
    {
        private readonly Mock<IAsyncRepository<Checkout>> _mockCheckoutRepository;

        public GetOrCreateCheckout()
        {
            _mockCheckoutRepository = new Mock<IAsyncRepository<Checkout>>();
        }

        [Fact]
        public async Task GetExistingCheckout()
        {
            var checkout = new Checkout();
            _mockCheckoutRepository.Setup(m => m.FirstAsync(It.IsAny<CheckoutWithUnitsSpecification>())).ReturnsAsync(checkout);

            var checkoutService = new CheckoutService(_mockCheckoutRepository.Object);
            var checkoutResult = await checkoutService.GetOrCreateCheckout(checkout.Id);

            _mockCheckoutRepository.Verify(m => m.FirstAsync(It.IsAny<CheckoutWithUnitsSpecification>()), Times.Once);
            Assert.Equal(checkout.Id, checkoutResult.Id);
            Assert.Equal(checkout.Date, checkoutResult.Date);
        }

        [Fact]
        public async Task CreateNewCheckout()
        {
            var checkout = new Checkout();
            _mockCheckoutRepository.Setup(m => m.AddAsync(It.IsAny<Checkout>())).ReturnsAsync(checkout);

            var checkoutService = new CheckoutService(_mockCheckoutRepository.Object);
            var checkoutResult = await checkoutService.GetOrCreateCheckout(null);

            _mockCheckoutRepository.Verify(m => m.AddAsync(It.IsAny<Checkout>()), Times.Once);
            Assert.Equal(checkout.Id, checkoutResult.Id);
            Assert.Equal(checkout.Date, checkoutResult.Date);
        }

    }
}
