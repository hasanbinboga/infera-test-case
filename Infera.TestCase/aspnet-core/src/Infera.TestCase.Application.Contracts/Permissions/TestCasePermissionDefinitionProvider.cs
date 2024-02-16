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

        var roomsPermission = inferaTestGroup.AddPermission(TestCasePermissions.Rooms.Default, L("Permission:Rooms"));
        roomsPermission.AddChild(TestCasePermissions.Rooms.Create, L("Permission:Rooms.Create"));
        roomsPermission.AddChild(TestCasePermissions.Rooms.Edit, L("Permission:Rooms.Edit"));
        roomsPermission.AddChild(TestCasePermissions.Rooms.Delete, L("Permission:Rooms.Delete"));


        var warehousesPermission = inferaTestGroup.AddPermission(TestCasePermissions.Warehouses.Default, L("Permission:Warehouses"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.Create, L("Permission:Warehouses.Create"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.Edit, L("Permission:Warehouses.Edit"));
        warehousesPermission.AddChild(TestCasePermissions.Warehouses.Delete, L("Permission:Warehouses.Delete"));


        var productsPermission = inferaTestGroup.AddPermission(TestCasePermissions.ProductInventories.Default, L("Permission:ProductInventories"));
        productsPermission.AddChild(TestCasePermissions.ProductInventories.Create, L("Permission:ProductInventories.Create"));
        productsPermission.AddChild(TestCasePermissions.ProductInventories.Edit, L("Permission:ProductInventories.Edit"));
        productsPermission.AddChild(TestCasePermissions.ProductInventories.Delete, L("Permission:ProductInventories.Delete"));

        var IssuesPermission = inferaTestGroup.AddPermission(TestCasePermissions.Issues.Default, L("Permission:Issues"));
        IssuesPermission.AddChild(TestCasePermissions.Issues.Create, L("Permission:Issues.Create"));
        IssuesPermission.AddChild(TestCasePermissions.Issues.Edit, L("Permission:Issues.Edit"));
        IssuesPermission.AddChild(TestCasePermissions.Issues.Delete, L("Permission:Issues.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestCaseResource>(name);
    }
}
