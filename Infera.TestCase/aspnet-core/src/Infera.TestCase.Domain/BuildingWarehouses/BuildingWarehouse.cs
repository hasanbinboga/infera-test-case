using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class BuildingWarehouse : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid BuildingId { get; protected set; }
        public virtual Guid WarehouseId { get; protected set; }
        

        protected BuildingWarehouse()
        {
            /* This constructor is for ORMs to be used while getting the entity from database.
             * - No need to initialize the Rooms or Warehouses collection
             *   since it will be overrided from the database.
             * - It's protected since proxying and deserialization tools
             *   may not work with private constructors.
            */
        }

        //Primary constructor
        internal BuildingWarehouse(Guid id, 
                        [NotNull] Guid buildingId,
                        [NotNull] Guid warehouseId): base(id) 
        {
            SetBuildingId(buildingId);
            SetWarehouseId(warehouseId);
        }


        internal void SetBuildingId(Guid buildingId)
        {
            Check.NotNull(buildingId, nameof(buildingId));
            BuildingId = buildingId;
        }

        internal void SetWarehouseId(Guid warehouseId)
        {
            Check.NotNull(warehouseId, nameof(warehouseId));
            WarehouseId = warehouseId;
        }

    }
}
