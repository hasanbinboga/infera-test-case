using Infera.TestCase.Buildings;
using Infera.TestCase.BuildingWarehouses;
using Infera.TestCase.Issues;
using Infera.TestCase.ProductInventories;
using Infera.TestCase.Rooms;
using Infera.TestCase.SaleOrders;
using Infera.TestCase.WarehouseInventories;
using Infera.TestCase.Warehouses;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Infera.TestCase.EntityFrameworkCore;

[DependsOn(
    typeof(TestCaseDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
public class TestCaseEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        TestCaseEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<TestCaseDbContext>(options =>
        {
            /* Remove "includeAllEntities: true" to create
             * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);

            options.AddRepository<Building, EfCoreBuildingRepository>();
            options.AddRepository<BuildingWarehouse, EfCoreBuildingWarehouseRepository>();
            options.AddRepository<Issue, EfCoreIssueRepository>();
            options.AddRepository<ProductInventory, EfCoreProductInventoryRepository>();
            options.AddRepository<Room, EfCoreRoomRepository>();
            options.AddRepository<SaleOrder, EfCoreSaleOrderRepository>();
            options.AddRepository<WarehouseInventory, EfCoreWarehouseInventoryRepository>();
            options.AddRepository<Warehouse, EfCoreWarehouseRepository>();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also TestCaseMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });

    }
}
