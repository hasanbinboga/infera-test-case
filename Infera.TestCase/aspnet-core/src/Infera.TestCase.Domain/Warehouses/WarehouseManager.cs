using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Infera.TestCase.Warehouses;

public class WarehouseManager : DomainService
{
    public IWarehouseRepository WarehouseRepository { get; }

    public WarehouseManager(IWarehouseRepository roomRepository)
    {
        WarehouseRepository = roomRepository; 
    }

    public async Task<Warehouse> CreateAsync(Guid? buildingId,
                        string name,
                        string no,
                        int floor,
                        int capacity,
                        string content,
                        string notes)
    {
        var existingWarehouse= await WarehouseRepository.FindWarehouse(buildingId, name, no);

        if (existingWarehouse != null)
        {
            throw new BusinessException(TestCaseDomainErrorCodes.WarehouseAlreadyExists);
        }
        return new Warehouse(GuidGenerator.Create(), buildingId, name, no, floor, capacity, content, notes);
    }
}
