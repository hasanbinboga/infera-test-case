using Volo.Abp.Modularity;

namespace Infera.TestCase;

[DependsOn(
    typeof(TestCaseDomainModule),
    typeof(TestCaseTestBaseModule)
)]
public class TestCaseDomainTestModule : AbpModule
{

}
