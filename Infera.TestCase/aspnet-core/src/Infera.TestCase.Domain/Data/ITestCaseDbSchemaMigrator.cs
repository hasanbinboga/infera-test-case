using System.Threading.Tasks;

namespace Infera.TestCase.Data;

public interface ITestCaseDbSchemaMigrator
{
    Task MigrateAsync();
}
