using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarketCheckout.Core.Interfaces
{
    public interface ISkuService
    {
        Task<decimal> CalculatePrice(DateTime date, int skuId, int numberOfUnits);

        Task<decimal> CalculatePrice(DateTime date, IEnumerable<(int skuId, int numberOfUnits)> units);
    }
}