using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class Room : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid BuildingId { get; set; }
        public virtual int Number { get; set; }
        public virtual int Capacity { get; set; }
        public virtual int Floor { get; set; }
        public virtual bool HasMiniBar { get; set; }
        public virtual string? Notes { get; set; }

        public Room()
        {
            
        }
    }
}
