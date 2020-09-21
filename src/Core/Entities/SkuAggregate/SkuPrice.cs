using Ardalis.GuardClauses;
using System;

namespace SupermarketCheckout.Core.Entities
{
    public class SkuPrice : BaseEntity
    {
        public int UnitsNumber { get; private set; }
        public decimal PricePerUnit { get; private set; }
        public DateTime? OfferStart { get; private set; }
        public DateTime? OfferEnd { get; private set; }

        public int SkuId { get; private set; }

        public SkuPrice(int unitsNumber, decimal pricePerUnit, DateTime? offerStart = null, DateTime? offerEnd = null)
        {
            Guard.Against.NegativeOrZero(unitsNumber, nameof(unitsNumber));
            Guard.Against.Negative(pricePerUnit, nameof(pricePerUnit));

            UnitsNumber = unitsNumber;
            PricePerUnit = pricePerUnit;
            OfferStart = offerStart;
            OfferEnd = offerEnd;
        }

        public void ModifyPricePerUnit(decimal newPricePerUnit)
        {
            Guard.Against.Negative(newPricePerUnit, nameof(newPricePerUnit));
            PricePerUnit = newPricePerUnit;
        }

        public bool IsBasePrice() => OfferStart == null && OfferEnd == null;

        public bool ExistOfferInRange(DateTime dateToCheck)
        {
            Guard.Against.OutOfSQLDateRange(dateToCheck, nameof(dateToCheck));
            return ((OfferStart <= dateToCheck) && ((!OfferEnd.HasValue) || (OfferEnd.Value > dateToCheck)));
        }
    }
}
