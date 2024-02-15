using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class Warehouse : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid BuildingId { get; set; }
        public virtual string Name { get; set; } 
        public virtual string No { get; set; } 
        public virtual int Floor { get; set; }
        public virtual int Capacity { get; set; }
        public virtual string? Content { get; set; }
        public virtual string? Notes { get; set; }

        protected Warehouse()
        {
            
        }


        public Warehouse(
                        Guid id,
                        [NotNull] Guid buildingId,
                        string name,
                        string no,
                        int floor,
                        int capacity,
                        string content,
                        string notes
                        ): base( id )
        {
            SetBuildingId(buildingId);
            SetName(name);
            SetNo(no);
            Floor = floor;
            Capacity = capacity;
            SetContent(content);
            SetNotes(notes);
        }


        internal void SetBuildingId( Guid buildingId )
        {
            Check.NotNull(buildingId, nameof(buildingId));
            BuildingId = buildingId;
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
