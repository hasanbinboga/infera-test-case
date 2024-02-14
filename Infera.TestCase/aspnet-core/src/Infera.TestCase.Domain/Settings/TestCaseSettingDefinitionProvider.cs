using Volo.Abp.Settings;

namespace Infera.TestCase.Settings;

public class TestCaseSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TestCaseSettings.MySetting1));
    }
}
