using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketCheckout.Core.Entities
{
    public class Checkout : BaseEntity
    {
        public DateTime Date { get; private set; }

        private readonly List<CheckoutUnit> _units = new List<CheckoutUnit>();
        public IReadOnlyCollection<CheckoutUnit> Units => _units.AsReadOnly();

        public Checkout()
        {
            Date = DateTime.UtcNow;
        }

        public int AddUnit(int skuId, int numberOfUnits = 1)
        {
            Guard.Against.NegativeOrZero(numberOfUnits, nameof(numberOfUnits));

            var existingUnit = _units.FirstOrDefault(u => u.SkuId == skuId);
            if (existingUnit==null)
            {
                _units.Add(new CheckoutUnit(numberOfUnits, skuId));
                return numberOfUnits;
            }
           return existingUnit.AddNumberOfUnits(numberOfUnits);
        }
    }
}
