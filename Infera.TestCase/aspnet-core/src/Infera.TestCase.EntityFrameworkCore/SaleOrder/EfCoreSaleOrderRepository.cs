using Infera.TestCase.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Infera.TestCase.SaleOrders;

internal class EfCoreSaleOrderRepository : EfCoreRepository<TestCaseDbContext, SaleOrder, Guid>, ISaleOrderRepository
{
    public EfCoreSaleOrderRepository(IDbContextProvider<TestCaseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    } 
}
