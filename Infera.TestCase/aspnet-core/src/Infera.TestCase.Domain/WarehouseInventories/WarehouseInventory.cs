using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class WarehouseInventory : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid WarehouseId { get; set; }
        public virtual Guid ProductInventoryId { get; set; }
        public virtual int Count { get; set; } 
        public virtual int Capacity { get; set; } 
        public virtual string? Notes { get; set; }

        public WarehouseInventory()
        {
            
        }
    }
}
