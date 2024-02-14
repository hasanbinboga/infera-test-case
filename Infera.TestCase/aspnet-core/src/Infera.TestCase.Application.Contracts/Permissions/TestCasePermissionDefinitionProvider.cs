using Infera.TestCase.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Infera.TestCase.Permissions;

public class TestCasePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TestCasePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(TestCasePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestCaseResource>(name);
    }
}
