using Infera.TestCase.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Infera.TestCase.Permissions;

public class TestCasePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var inferaTestGroup = context.AddGroup(TestCasePermissions.GroupName);

        var buildingsPermission = inferaTestGroup.AddPermission(TestCasePermissions.Buildings.Default, L("Permission:Buildings"));
        buildingsPermission.AddChild(TestCasePermissions.Buildings.Create, L("Permission:Buildings.Create"));
        buildingsPermission.AddChild(TestCasePermissions.Buildings.Edit, L("Permission:Buildings.Edit"));
        buildingsPermission.AddChild(TestCasePermissions.Buildings.Delete, L("Permission:Buildings.Delete"));
        buildingsPermission.AddChild(TestCasePermissions.Buildings.GetListOfWarehouseAsync, L("Permission:Buildings.GetListOfWarehouseAsync"));


        var buildingWarehousesPermission = inferaTestGroup.AddPermission(TestCasePermissions.BuildingWarehouses.Default, L("Permission:BuildingWarehouses"));
        buildingWarehousesPermission.AddChild(TestCasePermissions.BuildingWarehouses.Create, L("Permission:BuildingWarehouses.Create"));
        buildingWarehousesPermission.AddChild(TestCasePermissions.BuildingWarehouses.Edit, L("Permission:BuildingWarehouses.Edit"));
        buildingWarehousesPermission.AddChild(TestCasePermissions.BuildingWarehouses.Delete, L("Permission:BuildingWarehouses.Delete"));

        var roomsPermission = inferaTestGroup.AddPermission(TestCasePermissions.Rooms.Default, L("Permission:Rooms"));
        roomsPermission.AddChild(TestCasePermissions.Rooms.Create, L("Permission:Rooms.Create"));
        roomsPermission.AddChild(TestCasePermissions.Rooms.Edit, L("Permission:Rooms.Edit"));
        roomsPermission.AddChild(TestCasePermissions.Rooms.Delete, L("Permission:Rooms.Delete"));


        var warehousesPermission = inferaTestGroup.AddPermission(TestCasePermissions.Warehouses.Default, L("Permission:Warehouses"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.Create, L("Permission:Warehouses.Create"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.Edit, L("Permission:Warehouses.Edit"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.Delete, L("Permission:Warehouses.Delete"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.GetListOfBuildingAsync, L("Permission:Warehouses.GetListOfBuildingAsync"));

        var warehouseInventoriesPermission = inferaTestGroup.AddPermission(TestCasePermissions.WarehouseInventories.Default, L("Permission:WarehouseInventories"));
        warehouseInventoriesPermission.AddChild(TestCasePermissions.WarehouseInventories.Create, L("Permission:WarehouseInventories.Create"));
        warehouseInventoriesPermission.AddChild(TestCasePermissions.WarehouseInventories.Edit, L("Permission:WarehouseInventories.Edit"));
        warehouseInventoriesPermission.AddChild(TestCasePermissions.WarehouseInventories.Delete, L("Permission:WarehouseInventories.Delete"));
        warehouseInventoriesPermission.AddChild(TestCasePermissions.WarehouseInventories.GetListOfBuildingAsync, L("Permission:WarehouseInventories.GetListOfBuildingAsync"));


        var productsPermission = inferaTestGroup.AddPermission(TestCasePermissions.ProductInventories.Default, L("Permission:ProductInventories"));
        productsPermission.AddChild(TestCasePermissions.ProductInventories.Create, L("Permission:ProductInventories.Create"));
        productsPermission.AddChild(TestCasePermissions.ProductInventories.Edit, L("Permission:ProductInventories.Edit"));
        productsPermission.AddChild(TestCasePermissions.ProductInventories.Delete, L("Permission:ProductInventories.Delete"));

        var IssuesPermission = inferaTestGroup.AddPermission(TestCasePermissions.Issues.Default, L("Permission:Issues"));
        IssuesPermission.AddChild(TestCasePermissions.Issues.Create, L("Permission:Issues.Create"));
        IssuesPermission.AddChild(TestCasePermissions.Issues.Edit, L("Permission:Issues.Edit"));
        IssuesPermission.AddChild(TestCasePermissions.Issues.Delete, L("Permission:Issues.Delete"));

        var AccountingPermission = inferaTestGroup.AddPermission(TestCasePermissions.Accountings.Default, L("Permission:Accountings"));
        AccountingPermission.AddChild(TestCasePermissions.Accountings.Create, L("Permission:Accountings.Create"));
        AccountingPermission.AddChild(TestCasePermissions.Accountings.Edit, L("Permission:Accountings.Edit"));
        AccountingPermission.AddChild(TestCasePermissions.Accountings.Delete, L("Permission:Accountings.Delete"));


        var saleOrdersPermission = inferaTestGroup.AddPermission(TestCasePermissions.SaleOrders.Default, L("Permission:SaleOrders"));
        saleOrdersPermission.AddChild(TestCasePermissions.SaleOrders.Create, L("Permission:SaleOrders.Create"));
        saleOrdersPermission.AddChild(TestCasePermissions.SaleOrders.Edit, L("Permission:SaleOrders.Edit"));
        saleOrdersPermission.AddChild(TestCasePermissions.SaleOrders.Delete, L("Permission:SaleOrders.Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestCaseResource>(name);
    }
}
