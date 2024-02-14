using Infera.TestCase.Samples;
using Xunit;

namespace Infera.TestCase.EntityFrameworkCore.Domains;

[Collection(TestCaseTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TestCaseEntityFrameworkCoreTestModule>
{

}
