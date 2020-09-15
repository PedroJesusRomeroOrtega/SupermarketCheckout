using Ardalis.GuardClauses;

namespace SupermarketCheckout.Core.Entities
{
    public class CheckoutUnit : BaseEntity
    {
        public int NumberOfUnits { get; private set; }

        public int SkuId { get; private set; }

        public int CheckOutId { get; private set; }

        public CheckoutUnit(int numberOfUnits, int skuId)
        {
            Guard.Against.OutOfRange(numberOfUnits, nameof(numberOfUnits), 0, int.MaxValue);
            NumberOfUnits = numberOfUnits;
            SkuId = skuId;
        }

        public int AddNumberOfUnits(int numberOfUnits)
        {
            Guard.Against.OutOfRange(numberOfUnits, nameof(numberOfUnits), 0, int.MaxValue);
            NumberOfUnits += numberOfUnits;
            return NumberOfUnits;
        }
    }
}
