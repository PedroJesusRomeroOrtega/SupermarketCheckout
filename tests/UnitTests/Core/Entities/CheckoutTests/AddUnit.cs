using SupermarkerCheckout.Core.Entities;
using System.Linq;
using Xunit;

namespace SupermarkerCheckout.UnitTests.Core.Entities.CheckoutTests
{
    public class AddUnit
    {
        private readonly int _testSkuId = 1;
        private readonly int _numberOfUnits = 3;

        [Fact]
        public void AddNewUnit()
        {
            var checkout = new Checkout();
            checkout.AddUnit(_testSkuId,_numberOfUnits);

            var firstUnit = checkout.Units.Single();
            Assert.Equal(_testSkuId, firstUnit.SkuId);
            Assert.Equal(_numberOfUnits,firstUnit.NumberOfUnits);
        }

        [Fact]
        public void AddMoreUnitsForTheSameSku()
        {
            var checkout = new Checkout();
            checkout.AddUnit(_testSkuId, _numberOfUnits);
            checkout.AddUnit(_testSkuId, _numberOfUnits);

            var firstUnit = checkout.Units.Single();
            Assert.Equal(_testSkuId, firstUnit.SkuId);
            Assert.Equal(_numberOfUnits*2, firstUnit.NumberOfUnits);
        }
    }
}
