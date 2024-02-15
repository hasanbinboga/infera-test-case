using Volo.Abp.Domain.Services;

namespace Infera.TestCase.BuildingWarehouses;

public class BuildingWarehouseManager : DomainService
{
    public IBuildingWarehouseRepository BuildingWarehouseRepository { get; }

    public BuildingWarehouseManager(IBuildingWarehouseRepository buildingWarehouseRepository)
    {
        BuildingWarehouseRepository = buildingWarehouseRepository; 
    }

}
