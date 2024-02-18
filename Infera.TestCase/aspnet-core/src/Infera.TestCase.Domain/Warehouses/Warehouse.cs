using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class Warehouse : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid? BuildingId { get; protected set; }
        public virtual string Name { get; protected set; } 
        public virtual string No { get; protected set; } 
        public virtual int Floor { get; protected set; }
        public virtual int Capacity { get; protected set; }
        public virtual string? Content { get; protected set; }
        public virtual string? Notes { get; protected set; }
        public virtual Collection<BuildingWarehouse> BuildingWarehouses { get; protected set; } //Sub collection
        public virtual Collection<WarehouseInventory> WarehouseInventories { get; protected set; } //Sub collection

        protected Warehouse()
        {
            
        }


        internal Warehouse(
                        Guid id,
                        Guid? buildingId,
                        [NotNull] string name,
                        [NotNull] string no,
                        [NotNull] int floor,
                        [NotNull] int capacity,
                        string content,
                        string notes): base(id)
        {
            BuildingId = buildingId;
            SetName(name);
            SetNo(no);
            Floor = floor;
            Capacity = capacity;
            SetContent(content);
            SetNotes(notes);

            //initialize the collections

            BuildingWarehouses = new Collection<BuildingWarehouse>();
            WarehouseInventories = new Collection<WarehouseInventory>();
        }

 
        internal void SetName(string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name), WarehouseConsts.MaxNameLength, WarehouseConsts.MinNameLength);
            Name = name;
        }


        internal void SetNo(string no)
        {
            Check.NotNullOrWhiteSpace(no, nameof(no), WarehouseConsts.MaxNoLength);
            No = no;
        }

        internal void SetContent(string content)
        {
            Check.NotNullOrWhiteSpace(content, nameof(content), WarehouseConsts.MaxContentLength);
            Content = content;
        }

        internal void SetNotes(string notes)
        {
            Check.NotNullOrWhiteSpace(notes, nameof(notes), WarehouseConsts.MaxNotesLength);
            Notes = notes;
        }
    }
}
