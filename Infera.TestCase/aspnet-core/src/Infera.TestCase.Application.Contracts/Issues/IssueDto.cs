using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Issues;

public class IssueDto : AuditedEntityDto<Guid>
{
    public Guid? BuildingId { get; set; }
    public string? BuildingName { get; set; }
    public Guid? RoomBuildingId { get; set; }
    public string? RoomBuildingName { get; set; }
    public Guid? RoomId { get; set; }
    public int? RoomFloor { get; set; }
    public string? RoomNo { get; set; }
    public int? RoomCapacity { get; set; }
    public Guid? WarehouseInventoryId { get; set; }
    public string? WarehouseName { get; set; } 
    public string? WarehouseNo { get; set; } 
    public int? WarehouseFloor { get; set; } 
    public Guid? ProductInventoryId { get; set; }
    public string? ProductName { get; set; }
    public int? Number { get; set; }
    public IssueType Type { get; set; }
    public IssueEntityType EntityType { get; set; }
    public bool? IsCompleted { get; set; }
    public DateTime? CompletedTime { get; set; }
    public string? Notes { get; set; }
    public Guid? AssigneeId { get; set; }
    public string? AssigneeName { get; set; }
}
