using Infera.TestCase.Samples;
using Xunit;

namespace Infera.TestCase.EntityFrameworkCore.Applications;

[Collection(TestCaseTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TestCaseEntityFrameworkCoreTestModule>
{

}
