﻿using Ardalis.Specification;
using SupermarketCheckout.Core.Entities;

namespace SupermarketCheckout.Core.Specifications
{
    public sealed class CheckoutWithUnitsSpecification : Specification<Checkout>
    {

        public CheckoutWithUnitsSpecification()
        {
            // TODO: consider use pagination
            Query
                .Include(c => c.Units);
        }

        public CheckoutWithUnitsSpecification(int checkoutId)
        {
            Query
                .Where(c => c.Id == checkoutId)
                .Include(c => c.Units);
        }
    }
}
