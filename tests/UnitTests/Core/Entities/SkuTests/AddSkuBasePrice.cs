using Core.Entities;
using System.Linq;
using Xunit;

namespace UnitTests.Core.Entities.SkuTests
{
    public class AddSkuBasePrice
    {
        private readonly string _testSkuName = "A";
        private readonly decimal _testPricePerUnit = 50;
        private readonly decimal _testPricePerUnit2 = 60;

        [Fact]
        public void AddIfNotExist()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit);

            var firstSkuPrice = sku.SkuPrices.Single();

            Assert.Equal( _testPricePerUnit,firstSkuPrice.PricePerUnit);
        }

        [Fact]
        public void AddIfExist()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit);
            sku.AddSkuBasePrice(_testPricePerUnit2);

            Assert.Single(sku.SkuPrices);
            Assert.Equal( _testPricePerUnit2, sku.SkuPrices.Single().PricePerUnit);
        }
    }
}
