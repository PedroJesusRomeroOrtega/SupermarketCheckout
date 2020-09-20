using SupermarketCheckout.Core.Entities;
using System;
using Xunit;

namespace SupermarketCheckout.UnitTests.Core.Entities.SkuTests
{
    public class CalculatePrice
    {
        private readonly string _testSkuName = "A";
        private readonly decimal _testPricePerUnit1 = 50m;
        private readonly int _testNumberOfUnits2 = 3;
        private readonly decimal _testPricePerUnit2 = 43.3m;
        private readonly int _testNegativeNumberOfUnits = -2;
        private readonly DateTime _testActualDate = DateTime.UtcNow;

        [Fact]
        public void CalculateBasePrice()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit1);
            sku.AddSkuOfferPrice(_testNumberOfUnits2, _testPricePerUnit2, _testActualDate);

            var price= sku.CalculatePrice(_testActualDate);

            Assert.Equal(50, price);
        }

        [Fact]
        public void CalculatePriceWithoutOffers()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit1);

            var price = sku.CalculatePrice(_testActualDate,3);

            Assert.Equal(150, price);
        }

        [Fact]
        public void CalculatePriceWithoutDiscount()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit1);
            sku.AddSkuOfferPrice(_testNumberOfUnits2, _testPricePerUnit2, _testActualDate);

            var price = sku.CalculatePrice(_testActualDate,2);

            Assert.Equal(100, price);
        }

        [Fact]
        public void CalculatePriceWithExactUnitsForDiscount()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit1);
            sku.AddSkuOfferPrice(_testNumberOfUnits2, _testPricePerUnit2, _testActualDate);

            var price = sku.CalculatePrice(_testActualDate,3);

            Assert.Equal(130, decimal.Round(price));
        }

        [Fact]
        public void CalculatePriceWithDiscountAndOtherWithoutDiscount()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit1);
            sku.AddSkuOfferPrice(_testNumberOfUnits2, _testPricePerUnit2, _testActualDate);

            var price = sku.CalculatePrice(_testActualDate,4);

            Assert.Equal(180, decimal.Round(price));
        }

        [Fact]
        public void CanCalculateNegativeNumberOfUnits()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuBasePrice(_testPricePerUnit1);

            Assert.Throws<ArgumentException>(() => sku.CalculatePrice(_testActualDate,_testNegativeNumberOfUnits));
        }
    }
}
