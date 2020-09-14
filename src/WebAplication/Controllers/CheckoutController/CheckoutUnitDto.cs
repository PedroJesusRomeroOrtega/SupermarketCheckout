namespace SupermarketCheckout.WebAplication.Controllers.CheckoutController
{
    public class CheckoutUnitDto
    {
        public int? CheckOutId { get; set; }
        public int SkuId { get; set; }
        public int NumberOfUnits { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
