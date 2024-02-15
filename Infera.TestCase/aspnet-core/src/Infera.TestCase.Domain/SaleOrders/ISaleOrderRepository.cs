using System;
using Volo.Abp.Domain.Repositories;

namespace Infera.TestCase.SaleOrders;

public interface ISaleOrderRepository : IBasicRepository<SaleOrder, Guid>
{
    
}
