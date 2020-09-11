using System;

namespace Core.Entities
{
    public class SkuPrice: BaseEntity
    {
        public int MinUnitsNumber { get; set; }
        public decimal PricePerUnit { get; set; }
        public DateTime? OfferStart { get; set; }
        public DateTime? OfferEnd { get; set; }

        public int SkuId { get; set; }
        public Sku Sku { get; set; }
    }
}
