using System;
using Volo.Abp.Domain.Repositories;

namespace Infera.TestCase.SaleOrders;

public interface ISaleOrderRepository : IRepository<SaleOrder, Guid>
{
    
}
