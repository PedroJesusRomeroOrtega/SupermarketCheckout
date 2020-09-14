using SupermarketCheckout.Core.Entities;
using System;
using Xunit;

namespace SupermarketCheckout.UnitTests.Core.Entities.SkuTests
{
    public class CalculatePrice
    {
        private readonly string _testSkuName = "A";
        private readonly decimal _testPricePerUnit1 = 50m;
        private readonly decimal _testPricePerUnit2 = 43.3m;
        private readonly int _testNumberOfUnits2 = 3;
        private readonly DateTime _testActualDate = DateTime.Now;

        [Fact]
        public void CalculateBasePrice()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit1);
            sku.AddSkuOfferPrice(_testNumberOfUnits2, _testPricePerUnit2, _testActualDate);

            var price= sku.CalculatePrice();

            Assert.Equal(50, price);
        }

        [Fact]
        public void CalculatePriceWithoutDiscount()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit1);
            sku.AddSkuOfferPrice(_testNumberOfUnits2, _testPricePerUnit2, _testActualDate);

            var price = sku.CalculatePrice(2);

            Assert.Equal(100, price);
        }

        [Fact]
        public void CalculatePriceWithDiscount()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit1);
            sku.AddSkuOfferPrice(_testNumberOfUnits2, _testPricePerUnit2, _testActualDate);

            var price = sku.CalculatePrice(3);

            Assert.Equal(130, decimal.Round(price));
        }
    }
}
