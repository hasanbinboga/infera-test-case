using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Infera.TestCase.Warehouses;

public interface IWarehouseRepository : IBasicRepository<Warehouse, Guid>
{
    Task<Warehouse?> FindWarehouse(Guid? buildingId, string name, string no, CancellationToken cancellationToken = default);
}
