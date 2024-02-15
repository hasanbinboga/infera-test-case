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
            b.Property(s=>s.No).IsRequired().HasMaxLength(BuildingConsts.MaxNoLength);
            b.Property(s=>s.Addres).HasMaxLength(BuildingConsts.MaxAddresLength);

            
        });
        builder.Entity<Room>(b => 
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "Rooms", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.No).IsRequired().HasMaxLength(RoomConsts.MaxNoLength);
            b.Property(s => s.Floor).IsRequired();
            b.Property(s => s.Notes).HasMaxLength(RoomConsts.MaxNotesLength);

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne<Building>().WithMany().HasForeignKey(x => x.BuildingId).IsRequired();
        });

        builder.Entity<BuildingWarehouse>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "BuildingWarehouses", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.BuildingId).IsRequired();
            b.Property(s => s.WarehouseId).IsRequired();

            // ADD THE MAPPING FOR THE RELATION
            b.HasOne<Building>().WithMany().HasForeignKey(x => x.BuildingId).IsRequired();
            b.HasOne<Warehouse>().WithMany().HasForeignKey(x => x.WarehouseId).IsRequired();
        });

        builder.Entity<Warehouse>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "Warehouses", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Name).IsRequired().HasMaxLength(WarehouseConsts.MinNameLength);
            b.Property(s=>s.No).IsRequired();
            b.Property(s=>s.Floor).IsRequired();
            b.Property(s=>s.Notes).HasMaxLength(WarehouseConsts.MaxNotesLength);

            b.HasOne<Building>().WithMany().HasForeignKey(x => x.BuildingId).IsRequired();
        });

        builder.Entity<WarehouseInventory>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "WarehouseInventories", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Count).IsRequired();
            b.Property(s => s.Notes).HasMaxLength(WarehouseInventoryConsts.MaxNotesLength);

            b.HasOne<Warehouse>().WithMany().HasForeignKey(x => x.WarehouseId).IsRequired();
            b.HasOne<ProductInventory>().WithMany().HasForeignKey(x => x.ProductInventoryId).IsRequired();
        });

        builder.Entity<ProductInventory>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "ProductInventories", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Name).IsRequired().HasMaxLength(ProductInventoryConsts.MaxNameLength);
            b.Property(s => s.Manifacturer).HasMaxLength(ProductInventoryConsts.MaxManifacturerLength);
            b.Property(s => s.Size).IsRequired();
            b.Property(s => s.SalePrice).IsRequired();
        });

        builder.Entity<Accounting>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "Accountings", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Count).IsRequired();
            b.Property(s => s.SalePrice).IsRequired();
            b.Property(s => s.Amount).IsRequired();
            b.Property(s => s.Tax).IsRequired();
            b.Property(s => s.Type).IsRequired();
            

            b.HasOne<ProductInventory>().WithMany().HasForeignKey(x => x.ProductInventoryId).IsRequired();
            b.HasOne<SaleOrder>().WithMany().HasForeignKey(x => x.SaleOrderId).IsRequired();
        });

        builder.Entity<Room>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "Rooms", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.No).HasMaxLength(RoomConsts.MaxNoLength).IsRequired();
            b.Property(s => s.Floor).IsRequired();
            b.Property(s => s.Notes).HasMaxLength(RoomConsts.MaxNotesLength);

            b.HasOne<Building>().WithMany().HasForeignKey(x => x.BuildingId).IsRequired();
        });

        builder.Entity<SaleOrder>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "SaleOrders", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Count).IsRequired();
            b.Property(s => s.Notes).HasMaxLength(SaleOrderConsts.MaxNotesLength);

            b.HasOne<Room>().WithMany().HasForeignKey(x => x.RoomId).IsRequired();
            b.HasOne<WarehouseInventory>().WithMany().HasForeignKey(x => x.WarehouseInventoryId).IsRequired();
        });

        builder.Entity<Issue>(b =>
        {
            b.ToTable(TestCaseConsts.DbTablePrefix + "Issues", TestCaseConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props            
            b.Property(s => s.Type).IsRequired();
            b.Property(s => s.Notes).HasMaxLength(IssueConsts.MaxNotesLength);

            b.HasOne<Building>().WithMany().HasForeignKey(x => x.BuildingId);
            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.Assignee);
            b.HasOne<Room>().WithMany().HasForeignKey(x => x.RoomId);
            b.HasOne<WarehouseInventory>().WithMany().HasForeignKey(x => x.WarehouseInventoryId);
            b.HasOne<ProductInventory>().WithMany().HasForeignKey(x => x.ProductInventoryId);

        });

    }
}
