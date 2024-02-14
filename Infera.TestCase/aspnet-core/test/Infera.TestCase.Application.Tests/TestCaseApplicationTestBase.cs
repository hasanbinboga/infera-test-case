using Volo.Abp.Modularity;

namespace Infera.TestCase;

public abstract class TestCaseApplicationTestBase<TStartupModule> : TestCaseTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
