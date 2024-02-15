using Infera.TestCase.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Infera.TestCase.Issues;

internal class EfCoreIssueRepository : EfCoreRepository<TestCaseDbContext, Issue, Guid>, IIssueRepository
{
    public EfCoreIssueRepository(IDbContextProvider<TestCaseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    
}
