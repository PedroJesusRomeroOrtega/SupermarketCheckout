﻿using SupermarkerCheckout.Core.Entities;
using System;
using System.Linq;
using Xunit;

namespace SupermarkerCheckout.UnitTests.Core.Entities.SkuTests
{
    public class AddSkuOfferPrice
    {
        private readonly string _testSkuName = "A";
        private readonly decimal _testPricePerUnit = 43.3m;
        private readonly int _testMinNumberUnits1 = 3;
        private readonly int _testMinNumberUnits2 = 4;
        private readonly DateTime _testOfferStart1 = new DateTime(2020, 9, 5);
        private readonly DateTime _testOfferStart2 = new DateTime(2020, 8, 5);
        private readonly DateTime _testOfferEnd2 = new DateTime(2020, 9, 4);
        private readonly DateTime _testOfferStart3 = new DateTime(2020, 9, 6);

        [Fact]
        public void AddIfNotExistInSamePeriod()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuOfferPrice(_testMinNumberUnits1, _testPricePerUnit, _testOfferStart1);

            var firstSkuPrice = sku.SkuPrices.Single();

            Assert.Equal(firstSkuPrice.OfferStart, _testOfferStart1);
        }

        [Fact]
        public void AddIfExistOtherInDifferentPeriodSameUnits()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuOfferPrice(_testMinNumberUnits1, _testPricePerUnit, _testOfferStart1);
            sku.AddSkuOfferPrice(_testMinNumberUnits1, _testPricePerUnit, _testOfferStart2, _testOfferEnd2);

            Assert.Equal(2, sku.SkuPrices.Count());
        }

        [Fact]
        public void AddIfExistOtherInSamePeriodDifferentUnits()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuOfferPrice(_testMinNumberUnits1, _testPricePerUnit, _testOfferStart1);
            sku.AddSkuOfferPrice(_testMinNumberUnits2, _testPricePerUnit, _testOfferStart1);

            Assert.Equal(2, sku.SkuPrices.Count());
        }

        [Fact]
        public void CanAddIfExistOtherInSamePeriodSameUnits()
        {
            var sku = new Sku(_testSkuName);
            sku.AddSkuOfferPrice(_testMinNumberUnits1, _testPricePerUnit, _testOfferStart1);

            Assert.Throws<Exception>(() => sku.AddSkuOfferPrice(_testMinNumberUnits1, _testPricePerUnit, _testOfferStart3));
        }
    }
}
