using Infera.TestCase.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Infera.TestCase.Warehouses;

internal class EfCoreWarehouseRepository : EfCoreRepository<TestCaseDbContext, Warehouse, Guid>, IWarehouseRepository
{
    public EfCoreWarehouseRepository(IDbContextProvider<TestCaseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    

  
    public async Task<Warehouse?> FindWarehouse(Guid? buildingId, string name, string no, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetQueryableAsync();
        return await dbSet.FirstOrDefaultAsync(s =>
                            s.BuildingId == buildingId && s.Name == name && s.No == no,
                            GetCancellationToken(cancellationToken));
    }
}
