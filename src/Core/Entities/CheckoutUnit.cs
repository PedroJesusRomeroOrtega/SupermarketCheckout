namespace Core.Entities
{
    public class CheckoutUnit
    {
        public int NumberOfUnits { get; set; }

        public int SkuPriceId { get; set; }
        public SkuPrice SkuPrice { get; set; }

        public int CheckOutId { get; set; }
        public Checkout Checkout { get; set; }
    }
}
