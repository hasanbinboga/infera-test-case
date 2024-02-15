using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Infera.TestCase.BuildingWarehouses;

public class BuildingWarehouseManager : DomainService
{
    public IBuildingWarehouseRepository BuildingWarehouseRepository { get; }

    public BuildingWarehouseManager(IBuildingWarehouseRepository buildingWarehouseRepository)
    {
        BuildingWarehouseRepository = buildingWarehouseRepository; 
    }

    public async Task<BuildingWarehouse> CreateAsync(Guid buildingId, Guid warehouseId)
    {
        var existingBuilding = await BuildingWarehouseRepository.FindByBuildingAndWarehouseId(buildingId, warehouseId);

        if (existingBuilding != null)
        {
            throw new BusinessException(TestCaseDomainErrorCodes.BuildingWarehouseAlreadyExists);
        }

        return new BuildingWarehouse(GuidGenerator.Create(), buildingId, warehouseId);
    }
}
