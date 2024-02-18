using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class Issue : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid? BuildingId { get; set; }
        public virtual Guid? RoomId { get; set; }
        public virtual Guid? WarehouseInventoryId { get; set; }
        public virtual Guid? ProductInventoryId { get; set; }
        public virtual int? Number { get; set; }
        public virtual IssueEntityType EntityType { get; set; }
        public virtual IssueType Type { get; set; }
        public virtual bool? IsCompleted { get; set; }
        public virtual DateTime? CompletedTime { get; set; }
        public virtual string? Notes { get; set; }
        public virtual Guid? Assignee { get; set; }

        protected Issue()
        {

        }

        internal Issue(Guid id,
            Guid? buildingId,
            Guid? roomId,
            Guid? warehouseInventoryId,
            Guid? productInventoryId,
            [NotNull] IssueType type,
            [NotNull] IssueEntityType enitiyType,
            int? number,
            bool? isCompleted,
            DateTime? completedTime,
            string? notes,
            Guid? assignee) : base(id)
        {
            BuildingId = buildingId;
            RoomId = roomId;
            WarehouseInventoryId = warehouseInventoryId;
            ProductInventoryId = productInventoryId;
            Number = number;
            IsCompleted = isCompleted;
            CompletedTime = completedTime;
            SetNotes(notes);
            Assignee = assignee;
            SetType(type);
            SetEntityType(enitiyType);
        }

        internal void SetType(IssueType type)
        {   
            Check.NotNull(type, nameof(Type));
            Type = type;
        }

        internal void SetEntityType(IssueEntityType entityType)
        {
            Check.NotNull(entityType, nameof(EntityType));
            EntityType = entityType;
        }


        internal void SetNotes(string? notes)
        {
            Check.Length(notes, nameof(Notes), IssueConsts.MaxNotesLength);
            Notes = notes;
        }
    }
}
