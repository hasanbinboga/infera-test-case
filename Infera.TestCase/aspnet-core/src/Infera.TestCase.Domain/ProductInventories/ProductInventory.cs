using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class ProductInventory : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string  Manifacturer { get; set; }
        public virtual PorductInventoryType Type { get; set; }
        public virtual int Size { get; set; }
        public virtual bool SalePrice { get; set; }
        public virtual string? Notes { get; set; }

        public ProductInventory()
        {
            
        }
    }
}
