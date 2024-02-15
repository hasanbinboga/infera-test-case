using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class Issue : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid? BuildingId { get; set; }
        public virtual Guid? RoomId { get; set; }
        public virtual Guid? WarehouseInventoryId { get; set; }
        public virtual Guid? ProductInventoryId { get; set; }
        public virtual int Number { get; set; }
        public virtual bool IsCompleted { get; set; }
        public virtual DateTime? CompletedTime { get; set; }
        public virtual string? Notes { get; set; }
        public virtual Guid? Assignee { get; set; }

        public Issue()
        {
            
        }
    }
}
