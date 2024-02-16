using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Infera.TestCase.ProductInventories;

public interface IProductInventoryRepository : IRepository<ProductInventory, Guid>
{
    Task<ProductInventory?> FindProduct(string name, string manifacturer, int size, CancellationToken cancellationToken = default);
}
