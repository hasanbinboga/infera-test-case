using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class Room : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid BuildingId { get; set; }
        public virtual int Floor { get; set; }
        public virtual string No { get; set; }
        public virtual int Capacity { get; set; }
        public virtual bool HasMiniBar { get; set; }
        public virtual string? Notes { get; set; }

        protected Room()
        {
            
        }

        public Room(Guid id,
                    Guid buildingId, 
                    int floor,
                    string no,
                    int capacity,
                    bool hasMiniBar,
                    string notes
            ): base(id)
        {
            SetBuildingId(buildingId);
            Floor = floor;
            SetNo(no);
            Capacity = capacity;
            HasMiniBar = hasMiniBar;
            SetNotes(notes);
        }

        internal void SetBuildingId(Guid buildingId)
        {
            Check.NotNull(buildingId, nameof(buildingId));
            BuildingId = buildingId;
        }

        internal void SetNo(string no)
        {
            Check.NotNullOrWhiteSpace(no, nameof(no), RoomConsts.MaxNoLength);
            No = no;
        }

        internal void SetNotes(string notes)
        {
            Check.NotNullOrWhiteSpace(notes, nameof(notes), RoomConsts.MaxNotesLength);
            Notes = notes;
        }
    }
}
