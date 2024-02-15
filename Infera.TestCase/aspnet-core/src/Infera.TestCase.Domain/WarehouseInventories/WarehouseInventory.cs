using System;
using Volo.Abp;
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

        protected WarehouseInventory()
        {
            
        }

        public WarehouseInventory(Guid id,
                                  Guid warehouseId,
                                  Guid productInventoryId,
                                  int count,
                                  int capacity,
                                  string notes):base(id) 
        {
            WarehouseId = warehouseId;
            ProductInventoryId = productInventoryId;
            Capacity = capacity;
            Count = count;
            SetNotes(notes);

        }

        internal void SetNotes(string notes)
        {
            Check.NotNullOrWhiteSpace(notes, nameof(notes), WarehouseInventoryConsts.MaxNotesLength);
            Notes = notes;
        }


    }
}
