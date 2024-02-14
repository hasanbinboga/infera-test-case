using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infera.TestCase.Data;
using Volo.Abp.DependencyInjection;

namespace Infera.TestCase.EntityFrameworkCore;

public class EntityFrameworkCoreTestCaseDbSchemaMigrator
    : ITestCaseDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreTestCaseDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the TestCaseDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<TestCaseDbContext>()
            .Database
            .MigrateAsync();
    }
}
