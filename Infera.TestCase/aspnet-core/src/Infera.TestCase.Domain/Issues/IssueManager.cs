using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Infera.TestCase.Issues;

public class IssueManager : DomainService
{
    public IIssueRepository IssueRepository { get; }

    public IssueManager(IIssueRepository issueRepository)
    {
        IssueRepository = issueRepository; 
    }

    public Task<Issue> CreateAsync(Guid? buildingId,
            Guid? roomId,
            Guid? warehouseInventoryId,
            Guid? productInventoryId,
            IssueEntityType entityType,
            IssueType type,
            int? number,
            bool? isCompleted,
            DateTime? completedTime,
            string? notes,
            Guid? assignee)
    {
        return Task.FromResult(new Issue(GuidGenerator.Create(), buildingId, roomId, warehouseInventoryId, productInventoryId, type, entityType, number, isCompleted, completedTime, notes, assignee));
    }
}
