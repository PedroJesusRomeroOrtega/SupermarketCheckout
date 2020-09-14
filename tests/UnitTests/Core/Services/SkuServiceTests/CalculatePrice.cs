using Moq;
using SupermarketCheckout.Core.Entities;
using SupermarketCheckout.Core.Interfaces;
using SupermarketCheckout.Core.Services;
using System.Threading.Tasks;
using Xunit;

namespace SupermarketCheckout.UnitTests.Core.Services.SkuServiceTests
{
    public class CalculatePrice
    {
        private readonly string _testSkuName = "A";
        private readonly Mock<IAsyncRepository<Sku>> _mockSkuRepository;

        public CalculatePrice()
        {
            _mockSkuRepository = new Mock<IAsyncRepository<Sku>>();
        }

        [Fact]
        public async Task InvokeGetByIdAsync()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(It.IsAny<decimal>());
            _mockSkuRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(sku);

            var skuService = new SkuService(_mockSkuRepository.Object);
            await skuService.CalculatePrice(sku.Id, 1);

            _mockSkuRepository.Verify(m => m.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
