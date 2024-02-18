using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Infera.TestCase.WarehouseInventories;

public class WarehouseInventoryManager : DomainService
{
    public IWarehouseInventoryRepository WarehouseInventoryRepository { get; }

    public WarehouseInventoryManager(IWarehouseInventoryRepository roomRepository)
    {
        WarehouseInventoryRepository = roomRepository; 
    }

    public async Task<WarehouseInventory> CreateAsync(Guid warehouseId,
                                  Guid productInventoryId,
                                  int count,
                                  int capacity,
                                  string? notes)
    {
        var existingWarehouseInventory= await WarehouseInventoryRepository.FindByProductId(warehouseId, productInventoryId);

        if (existingWarehouseInventory != null)
        {
            throw new BusinessException(TestCaseDomainErrorCodes.WarehouseInventoryAlreadyExists);
        }

        return new WarehouseInventory(GuidGenerator.Create(), warehouseId, productInventoryId, count, capacity, notes);
    }
}
