using Infera.TestCase.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Infera.TestCase.Buildings;

internal class EfCoreBuildingRepository : EfCoreRepository<TestCaseDbContext, Building, Guid>, IBuildingRepository
{
    public EfCoreBuildingRepository(IDbContextProvider<TestCaseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<Building?> FindByName(string name, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetQueryableAsync();
        return await dbSet.FirstOrDefaultAsync(s => 
                            s.Name == name, 
                            GetCancellationToken(cancellationToken));
    }
}
