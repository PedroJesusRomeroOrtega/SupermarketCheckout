using System.Collections.Generic;

namespace Core.Entities
{
    public class Sku : BaseEntity
    {
        public string Name { get; set; }

        public List<Sku> Skus { get; set; }
    }
}
