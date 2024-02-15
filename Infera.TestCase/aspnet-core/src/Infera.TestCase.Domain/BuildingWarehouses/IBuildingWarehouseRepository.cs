using System;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories;

namespace Infera.TestCase.BuildingWarehouses;

public interface IBuildingWarehouseRepository : IBasicRepository<BuildingWarehouse, Guid>
{
    Task<BuildingWarehouse?> FindByBuildingId(Guid buildingId, CancellationToken cancellationToken = default);
    Task<BuildingWarehouse?> FindByWarehouseId(Guid warehouseId, CancellationToken cancellationToken = default);
    Task<BuildingWarehouse?> FindByBuildingAndWarehouseId(Guid buildingId, Guid warehouseId, CancellationToken cancellationToken = default);
}
