using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Infera.TestCase.WarehouseInventories;

public interface IWarehouseInventoryRepository : IRepository<WarehouseInventory, Guid>
{
    Task<WarehouseInventory?> FindByProductId(Guid warehouseId, Guid productId, CancellationToken cancellationToken = default);
}
