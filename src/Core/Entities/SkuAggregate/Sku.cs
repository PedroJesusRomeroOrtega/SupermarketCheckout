using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout.Core.Entities
{
    public class Sku : BaseEntity
    {
        public string Name { get; private set; }

        private readonly List<SkuPrice> _skuPrices = new List<SkuPrice>();
        public IReadOnlyCollection<SkuPrice> SkuPrices => _skuPrices.AsReadOnly();

        public Sku(string name)
        {
            Name = name;
        }

        public void AddSkuBasePrice(decimal pricePerUnit)
        {
            var baseSkuPrice = SkuPrices.FirstOrDefault(sp => sp.OfferStart == null && sp.OfferEnd == null);
            if (baseSkuPrice == null)
            {
                _skuPrices.Add(new SkuPrice(1, pricePerUnit));
                return;
            }
            baseSkuPrice.ModifyPricePerUnit(pricePerUnit);
        }

        public void AddSkuOfferPrice(int minUnitsNumber, decimal pricePerUnit, DateTime offerStart, DateTime? offerEnd = null)
        {
            //TODO: minUnitsNumber must be greater than 1.
            if (SkuPrices.Any(sp => sp.MinUnitsNumber == minUnitsNumber && sp.OfferStart.HasValue && sp.ExistInRange(sp.OfferStart.Value, sp.OfferEnd, offerStart)))
            {
                //TODO: creates custom exceptions
                throw new Exception("There are other offer for the same period");
            }

            _skuPrices.Add(new SkuPrice(minUnitsNumber, pricePerUnit, offerStart, offerEnd));
        }

        public decimal CalculatePrice(int numberOfUnits = 1)
        {
            //TODO: add guard numberunits more than 0
           var skuPrice= SkuPrices
                .OrderByDescending(sp=>sp.MinUnitsNumber)
                .First(sp => sp.MinUnitsNumber <= numberOfUnits);
            return skuPrice.PricePerUnit * numberOfUnits;
        }
    }
}
