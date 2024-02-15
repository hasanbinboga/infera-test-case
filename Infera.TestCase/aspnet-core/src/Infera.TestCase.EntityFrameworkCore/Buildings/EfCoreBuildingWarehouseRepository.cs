using Infera.TestCase.BuildingWarehouses;
using Infera.TestCase.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Infera.TestCase.Buildings;

internal class EfCoreBuildingWarehouseRepository : EfCoreRepository<TestCaseDbContext, BuildingWarehouse, Guid>, IBuildingWarehouseRepository
{
    public EfCoreBuildingWarehouseRepository(IDbContextProvider<TestCaseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

}
