using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Infera.TestCase.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class TestCaseDbContext :
    AbpDbContext<TestCaseDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    #region TestCase entities
    public DbSet<Building> Buildings { get; set; }
    public DbSet<BuildingWarehouse> BuildingWarehouses { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<WarehouseInventory> WarehouseInventories { get; set; }
    public DbSet<ProductInventory> ProductInventories { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<SaleOrder> saleOrders { get; set; }
    public DbSet<Accounting> Accountings { get; set; }
    public DbSet<Issue> Issues { get; set; }

    #endregion

    public TestCaseDbContext(DbContextOptions<TestCaseDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<Building>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "Buildings", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(s => s.Name).IsRequired().HasMaxLength(BuildingConsts.MaxNameLength);
            b.Property(s => s.No).IsRequired().HasMaxLength(BuildingConsts.MaxNoLength);
            b.Property(s => s.Addres).HasMaxLength(BuildingConsts.MaxAddresLength);
        });

        builder.Entity<Building>().HasIndex(s => new { s.Name, s.No, s.IsDeleted });

        builder.Entity<Room>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "Rooms", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.No).IsRequired().HasMaxLength(RoomConsts.MaxNoLength);
            b.Property(s => s.Floor).IsRequired();
            b.Property(s => s.Notes).HasMaxLength(RoomConsts.MaxNotesLength);

        }); 

        builder.Entity<Room>().HasIndex(s => new { s.BuildingId, s.No, s.Floor, s.HasMiniBar, s.Capacity, s.IsDeleted });

        builder.Entity<BuildingWarehouse>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "BuildingWarehouses", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.BuildingId).IsRequired();
            b.Property(s => s.WarehouseId).IsRequired();

        });
        
        builder.Entity<BuildingWarehouse>().HasIndex(s => new { s.BuildingId, s.WarehouseId, s.IsDeleted });

        builder.Entity<Warehouse>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "Warehouses", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Name).IsRequired().HasMaxLength(WarehouseConsts.MaxNameLength);
            b.Property(s => s.No).IsRequired();
            b.Property(s => s.Floor).IsRequired();
            b.Property(s => s.Notes).HasMaxLength(WarehouseConsts.MaxNotesLength);

        });

        builder.Entity<Warehouse>().HasIndex(s => new { s.BuildingId, s.Name, s.No, s.Floor, s.Capacity, s.IsDeleted });

        builder.Entity<WarehouseInventory>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "WarehouseInventories", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Count).IsRequired();
            b.Property(s => s.Notes).HasMaxLength(WarehouseInventoryConsts.MaxNotesLength);

        });

        builder.Entity<WarehouseInventory>().HasIndex(s => new { s.WarehouseId, s.ProductInventoryId, s.Count, s.Capacity, s.IsDeleted });

        builder.Entity<ProductInventory>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "ProductInventories", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Name).IsRequired().HasMaxLength(ProductInventoryConsts.MaxNameLength);
            b.Property(s => s.Manifacturer).HasMaxLength(ProductInventoryConsts.MaxManifacturerLength);
            b.Property(s => s.Size).IsRequired();
            b.Property(s => s.SalePrice).IsRequired();
        });

        builder.Entity<ProductInventory>().HasIndex(s => new { s.Name, s.Manifacturer, s.Type, s.Size, s.SalePrice, s.IsDeleted });


        builder.Entity<Accounting>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "Accountings", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Count).IsRequired();
            b.Property(s => s.SalePrice).IsRequired();
            b.Property(s => s.Amount).IsRequired();
            b.Property(s => s.Tax).IsRequired();
            b.Property(s => s.Type).IsRequired();
        });

        builder.Entity<Accounting>().HasIndex(s => new { s.ProductInventoryId, s.SaleOrderId, s.Type, s.InvoiceDate, s.InvoiceNumber, s.SalePrice, s.PurchasePrice, s.IsDeleted });

        builder.Entity<SaleOrder>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "SaleOrders", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Count).IsRequired();
            b.Property(s => s.Notes).HasMaxLength(SaleOrderConsts.MaxNotesLength);
        });

        builder.Entity<SaleOrder>().HasIndex(s => new { s.RoomId, s.WarehouseInventoryId, s.Count, s.IsCompleted, s.IsDeleted });

        builder.Entity<Issue>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "Issues", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Type).IsRequired();
            b.Property(s => s.Notes).HasMaxLength(IssueConsts.MaxNotesLength);

            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.Assignee);
        });

        builder.Entity<Issue>().HasIndex(s => new { s.BuildingId, s.RoomId, s.WarehouseInventoryId, s.ProductInventoryId, s.Type, s.Assignee, s.IsCompleted, s.CompletedTime, s.IsDeleted });
    }
}
