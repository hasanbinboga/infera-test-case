using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Infera.TestCase.Data;

/* This is used if database provider does't define
 * ITestCaseDbSchemaMigrator implementation.
 */
public class NullTestCaseDbSchemaMigrator : ITestCaseDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
