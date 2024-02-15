using Infera.TestCase.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Infera.TestCase.ProductInventories;

internal class EfCoreProductInventoryRepository : EfCoreRepository<TestCaseDbContext, ProductInventory, Guid>, IProductInventoryRepository
{
    public EfCoreProductInventoryRepository(IDbContextProvider<TestCaseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

     
    public async Task<ProductInventory?> FindProduct(string name, string manifacturer, int size, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetQueryableAsync();
        return await dbSet.FirstOrDefaultAsync(s =>
                            s.Name == name && s.Manifacturer == manifacturer && s.Size == size,
                            GetCancellationToken(cancellationToken));
    }
}
