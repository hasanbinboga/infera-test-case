using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Infera.TestCase.Issues;

public class IssueManager : DomainService
{
    public IIssueRepository IssueRepository { get; }

    public IssueManager(IIssueRepository IssueRepository)
    {
        IssueRepository = IssueRepository; 
    }

    public async Task<Issue> CreateAsync(Guid buildingId,
            Guid roomId,
            Guid warehouseInventoryId,
            Guid productInventoryId,
            IssueType type,
            int number,
            bool isCompleted,
            DateTime? completedTime,
            string notes,
            Guid assignee)
    {
        return new Issue(GuidGenerator.Create(), buildingId, roomId, warehouseInventoryId, productInventoryId, type, number, isCompleted, completedTime, notes, assignee);
    }
}
