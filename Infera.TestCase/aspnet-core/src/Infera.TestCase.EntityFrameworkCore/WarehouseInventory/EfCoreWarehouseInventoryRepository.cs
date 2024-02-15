using Infera.TestCase.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Infera.TestCase.WarehouseInventories;

internal class EfCoreWarehouseInventoryRepository : EfCoreRepository<TestCaseDbContext, WarehouseInventory, Guid>, IWarehouseInventoryRepository
{
    public EfCoreWarehouseInventoryRepository(IDbContextProvider<TestCaseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<WarehouseInventory?> FindByProductId(Guid warehouseId, Guid productId, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetQueryableAsync();
        return await dbSet.FirstOrDefaultAsync(s =>
                            s.WarehouseId == warehouseId && s.ProductInventoryId == productId,
                            GetCancellationToken(cancellationToken));
    }
}
