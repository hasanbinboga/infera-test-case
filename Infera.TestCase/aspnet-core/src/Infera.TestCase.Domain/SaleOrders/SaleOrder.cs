using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class SaleOrder : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid RoomId { get; set; }
        public virtual Guid WarehouseInventoryId { get; set; }
        public virtual int Count { get; set; }
        public virtual bool IsCompleted { get; set; }
        public virtual string? Notes { get; set; }

        public SaleOrder()
        {
            
        }
    }
}
