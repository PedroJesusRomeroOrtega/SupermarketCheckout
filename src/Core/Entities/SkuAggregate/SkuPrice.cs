using System;

namespace SupermarketCheckout.Core.Entities
{
    public class SkuPrice : BaseEntity
    {
        public int MinUnitsNumber { get; private set; }
        public decimal PricePerUnit { get; private set; }
        public DateTime? OfferStart { get; private set; }
        public DateTime? OfferEnd { get; private set; }

        public int SkuId { get; private set; }

        public SkuPrice(int minUnitsNumber, decimal pricePerUnit, DateTime? offerStart = null, DateTime? offerEnd = null)
        {
            MinUnitsNumber = minUnitsNumber;
            PricePerUnit = pricePerUnit;
            OfferStart = offerStart;
            OfferEnd = offerEnd;
        }

        public void ModifyPricePerUnit(decimal newPricePerUnit)
        {
            PricePerUnit = newPricePerUnit;
        }

        public bool ExistInRange(DateTime rangeStart, DateTime? rangeEnd, DateTime dateToCheck)
        {
            return ((rangeStart < dateToCheck) && ((!rangeEnd.HasValue) || (rangeEnd.Value > dateToCheck)));
        }
    }
}
