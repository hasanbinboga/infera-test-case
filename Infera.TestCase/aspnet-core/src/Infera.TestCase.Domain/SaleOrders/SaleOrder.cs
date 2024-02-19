using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
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
        public virtual Collection<Accounting> Accountings { get; protected set; } //Sub collection

        protected SaleOrder()
        {

        }

        internal SaleOrder(Guid id,
                        [NotNull] Guid roomId,
                        [NotNull] Guid warehouseInventoryId,
                        [NotNull] int count,
                        bool isCompleted,
                        string? notes
                        ) : base(id)
        {
            SetRoomId(roomId);
            SetWarehouseInventoryId(warehouseInventoryId);
            SetCount(count);
            IsCompleted = isCompleted;
            SetNotes(notes);

            //initialize the collections

            Accountings = new Collection<Accounting>();
        }

        internal void SetRoomId(Guid roomId)
        {
            Check.NotNull(roomId, nameof(roomId));
            RoomId = roomId;
        }

        internal void SetWarehouseInventoryId(Guid warehouseInventoryId)
        {
            Check.NotNull(warehouseInventoryId, nameof(warehouseInventoryId));
            WarehouseInventoryId = warehouseInventoryId;
        }

        internal void SetCount(int count)
        {
            Check.NotNull(count, nameof(count));
            Count = count;
        }


        internal void SetNotes(string? notes)
        {
            if (Notes != null)
            {
                Check.Length(notes, nameof(notes), SaleOrderConsts.MaxNotesLength);
            }
            Notes = notes;
        }



    }
}
