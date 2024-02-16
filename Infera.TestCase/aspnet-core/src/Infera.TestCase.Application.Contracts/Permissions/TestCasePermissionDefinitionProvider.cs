using Infera.TestCase.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Infera.TestCase.Permissions;

public class TestCasePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var inferaTestGroup = context.AddGroup(TestCasePermissions.GroupName);

        var booksPermission = inferaTestGroup.AddPermission(TestCasePermissions.Buildings.Default, L("Permission:Buildings"));
        booksPermission.AddChild(TestCasePermissions.Buildings.Create, L("Permission:Buildings.Create"));
        booksPermission.AddChild(TestCasePermissions.Buildings.Edit, L("Permission:Buildings.Edit"));
        booksPermission.AddChild(TestCasePermissions.Buildings.Delete, L("Permission:Buildings.Delete"));

        var authorsPermission = inferaTestGroup.AddPermission(TestCasePermissions.Rooms.Default, L("Permission:Rooms"));
        authorsPermission.AddChild(TestCasePermissions.Rooms.Create, L("Permission:Rooms.Create"));
        authorsPermission.AddChild(TestCasePermissions.Rooms.Edit, L("Permission:Rooms.Edit"));
        authorsPermission.AddChild(TestCasePermissions.Rooms.Delete, L("Permission:Rooms.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestCaseResource>(name);
    }
}
