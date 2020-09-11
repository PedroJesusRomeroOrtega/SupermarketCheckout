using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Checkout: BaseEntity
    {
        public DateTime DateTime { get; set; }

        public List<CheckoutUnit> CheckoutUnits { get; set; }
    }
}
