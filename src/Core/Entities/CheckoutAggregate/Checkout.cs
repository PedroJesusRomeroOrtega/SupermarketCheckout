using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
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

        public void AddUnit(int skuId, int numberOfUnits = 1)
        {
            var existingUnit = _units.FirstOrDefault(u => u.SkuId == skuId);
            if (existingUnit==null)
            {
                _units.Add(new CheckoutUnit(numberOfUnits, skuId));
                return;
            }
            existingUnit.AddNumberOfUnits(numberOfUnits);
        }
    }
}
