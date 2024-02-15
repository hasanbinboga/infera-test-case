using Infera.TestCase.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Infera.TestCase.BuildingWarehouses;

internal class EfCoreBuildingWarehouseRepository : EfCoreRepository<TestCaseDbContext, BuildingWarehouse, Guid>, IBuildingWarehouseRepository
{
    public EfCoreBuildingWarehouseRepository(IDbContextProvider<TestCaseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<BuildingWarehouse?> FindByBuildingAndWarehouseId(Guid buildingId, Guid warehouseId, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetQueryableAsync();
        return await dbSet.FirstOrDefaultAsync(s =>
                            s.BuildingId == buildingId && s.WarehouseId == warehouseId,
                            GetCancellationToken(cancellationToken));
    }

    public async Task<BuildingWarehouse?> FindByBuildingId(Guid buildingId, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetQueryableAsync();
        return await dbSet.FirstOrDefaultAsync(s =>
                            s.BuildingId == buildingId,
                            GetCancellationToken(cancellationToken));
    }

    public async Task<BuildingWarehouse?> FindByWarehouseId(Guid warehouseId, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetQueryableAsync();
        return await dbSet.FirstOrDefaultAsync(s =>
                            s.WarehouseId == warehouseId,
                            GetCancellationToken(cancellationToken));
    }
}
