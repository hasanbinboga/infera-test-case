using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class Warehouse : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid BuildingId { get; set; }
        public virtual string Name { get; set; } 
        public virtual  int Number { get; set; } 
        public virtual int Floor { get; set; }
        public virtual int Capacity { get; set; }
        public virtual string? Content { get; set; }
        public virtual string? Notes { get; set; }

        public Warehouse()
        {
            
        }

    }
}
