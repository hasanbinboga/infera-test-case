using Xunit;

namespace Infera.TestCase.EntityFrameworkCore;

[CollectionDefinition(TestCaseTestConsts.CollectionDefinitionName)]
public class TestCaseEntityFrameworkCoreCollection : ICollectionFixture<TestCaseEntityFrameworkCoreFixture>
{

}
