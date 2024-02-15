using System;
using Volo.Abp.Domain.Repositories;

namespace Infera.TestCase.Issues;

public interface IIssueRepository : IBasicRepository<Issue, Guid>
{
  
}
