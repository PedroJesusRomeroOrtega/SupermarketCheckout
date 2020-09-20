using Ardalis.GuardClauses;
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

        public void AddSkuOfferPrice(int UnitsNumber, decimal pricePerUnit, DateTime offerStart, DateTime? offerEnd = null)
        {
            Guard.Against.OutOfRange(UnitsNumber, nameof(UnitsNumber), 0, int.MaxValue);

            if (SkuPrices.Any(sp => !sp.IsBasePrice()
            && sp.ExistOfferInRange(offerStart)))
            {
                //TODO: creates custom exceptions
                throw new Exception("There are other offer for the same period");
            }

            _skuPrices.Add(new SkuPrice(UnitsNumber, pricePerUnit, offerStart, offerEnd));
        }

        public decimal CalculatePrice(DateTime date, int numberOfUnits = 1)
        {
            Guard.Against.OutOfRange(numberOfUnits, nameof(numberOfUnits), 0, int.MaxValue);

            var offerPrice = SkuPrices.FirstOrDefault(sp => !sp.IsBasePrice() && sp.ExistOfferInRange(date));
            var basePrice = SkuPrices.First(sp => sp.IsBasePrice());

            var unitsWithoutOffer = numberOfUnits;
            var priceWithOffer = 0m;
            if (offerPrice != null)
            {
                var unitsWithOffer = (numberOfUnits / offerPrice.UnitsNumber) * offerPrice.UnitsNumber;
                priceWithOffer = unitsWithOffer * offerPrice.PricePerUnit;
                unitsWithoutOffer = numberOfUnits % offerPrice.UnitsNumber;
            }

            var priceWithoutOffer = unitsWithoutOffer * basePrice.PricePerUnit;

            return priceWithOffer + priceWithoutOffer;
        }
    }
}
