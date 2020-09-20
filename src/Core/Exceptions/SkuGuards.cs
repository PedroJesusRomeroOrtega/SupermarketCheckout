using Ardalis.GuardClauses;
using SupermarketCheckout.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Exceptions
{
    public static class SkuGuards
    {
        public static void OverlapOffer(this IGuardClause guardClause, IEnumerable<SkuPrice> skuPrice, DateTime offerStart)
        {
            if (skuPrice.Any(sp => !sp.IsBasePrice() && sp.ExistOfferInRange(offerStart)))
            {
                throw new OverlapOfferException();
            }
        }
    }
}
