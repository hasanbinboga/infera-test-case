using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Infera.TestCase;

[Dependency(ReplaceServices = true)]
public class TestCaseBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TestCase";
}
