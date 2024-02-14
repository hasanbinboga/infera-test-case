using Volo.Abp.Modularity;

namespace Infera.TestCase;

[DependsOn(
    typeof(TestCaseApplicationModule),
    typeof(TestCaseDomainTestModule)
)]
public class TestCaseApplicationTestModule : AbpModule
{

}
