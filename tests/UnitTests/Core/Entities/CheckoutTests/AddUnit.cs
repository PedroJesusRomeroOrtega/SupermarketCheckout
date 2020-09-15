using SupermarketCheckout.Core.Entities;
using System.Linq;
using Xunit;

namespace SupermarketCheckout.UnitTests.Core.Entities.CheckoutTests
{
    public class AddUnit
    {
        private readonly int _testSkuId = 1;
        private readonly int _testNumberOfUnits = 3;

        [Fact]
        public void AddNewUnit()
        {
            var checkout = new Checkout();
            int numberOfUnitsReturned = checkout.AddUnit(_testSkuId, _testNumberOfUnits);

            var firstUnit = checkout.Units.Single();
            Assert.Equal(_testSkuId, firstUnit.SkuId);
            Assert.Equal(_testNumberOfUnits, firstUnit.NumberOfUnits);
            Assert.Equal(_testNumberOfUnits, numberOfUnitsReturned);
        }

        [Fact]
        public void AddMoreUnitsForTheSameSku()
        {
            var checkout = new Checkout();
            checkout.AddUnit(_testSkuId, _testNumberOfUnits);
            checkout.AddUnit(_testSkuId, _testNumberOfUnits);

            var firstUnit = checkout.Units.Single();
            Assert.Equal(_testSkuId, firstUnit.SkuId);
            Assert.Equal(_testNumberOfUnits * 2, firstUnit.NumberOfUnits);
        }
    }
}
