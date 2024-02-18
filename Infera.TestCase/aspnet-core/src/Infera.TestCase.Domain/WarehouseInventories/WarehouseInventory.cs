using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class WarehouseInventory : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid WarehouseId { get; protected set; }
        public virtual Guid ProductInventoryId { get; protected set; }
        public virtual int Count { get; protected set; }
        public virtual int Capacity { get; protected set; }
        public virtual string? Notes { get; protected set; }

        public virtual Collection<SaleOrder> SaleOrders { get; protected set; } //Sub collection
        public virtual Collection<Issue> Issues { get; protected set; } //Sub collection

        protected WarehouseInventory()
        {

        }

        internal WarehouseInventory(Guid id,
                                 [NotNull] Guid warehouseId,
                                 [NotNull] Guid productInventoryId,
                                  int count,
                                  int capacity,
                                  string? notes) : base(id)
        {
            SetWarehouseId(warehouseId);
            SetProductInventoryId(productInventoryId);
            Capacity = capacity;
            Count = count;
            SetNotes(notes);

            //initialize the collections
            SaleOrders = new Collection<SaleOrder>();
            Issues = new Collection<Issue>();
        }

        internal void SetNotes(string? notes)
        {
            if (string.IsNullOrEmpty(notes) == false)
            {
                Check.NotNullOrWhiteSpace(notes, nameof(notes), WarehouseInventoryConsts.MaxNotesLength);
            }
            Notes = notes;
        }

        internal void SetWarehouseId(Guid warehouseId)
        {
            Check.NotNull(warehouseId, nameof(warehouseId));
            WarehouseId = warehouseId;
        }


        internal void SetProductInventoryId(Guid productInventoryId)
        {
            Check.NotNull(productInventoryId, nameof(productInventoryId));
            ProductInventoryId = productInventoryId;
        }

    }
}
