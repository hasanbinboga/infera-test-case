using Volo.Abp.Modularity;

namespace Infera.TestCase;

/* Inherit from this class for your domain layer tests. */
public abstract class TestCaseDomainTestBase<TStartupModule> : TestCaseTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
