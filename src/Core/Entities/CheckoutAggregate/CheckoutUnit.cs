namespace SupermarkerCheckout.Core.Entities
{
    public class CheckoutUnit
    {
        public int NumberOfUnits { get; private set; }

        public int SkuId { get; private set; }

        public int CheckOutId { get; private set; }

        public CheckoutUnit(int numberOfUnits,int skuId)
        {
            //TODO: guard number of units greater than 0
            NumberOfUnits = numberOfUnits;
            SkuId = skuId;
        }

        public void AddNumberOfUnits(int numberOfUnits)
        {
            //TODO: guard number of units greater than 0
            NumberOfUnits += numberOfUnits;
        }
    }
}
