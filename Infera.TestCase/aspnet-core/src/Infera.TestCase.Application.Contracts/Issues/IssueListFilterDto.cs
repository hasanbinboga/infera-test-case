using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Issues;

[Serializable]
public class IssueListFilterDto : PagedAndSortedResultRequestDto
{
    public IssueEntityType EntityType { get; set; }
    public Guid? BuildingId { get; set; }
    public Guid? RoomId { get; set; }
    public Guid? WarehouseInventoryId { get; set; }
    public Guid? ProductInventoryId { get; set; }
    
}
