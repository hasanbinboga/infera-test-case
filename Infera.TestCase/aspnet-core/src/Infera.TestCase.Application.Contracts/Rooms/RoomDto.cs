using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Rooms;

public class RoomDto: AuditedEntityDto<Guid>
{

    public Guid BuildingId { get; set; }
    public string BuildingName { get; set; } = null!;
    public int Floor { get; set; }
    public string No { get; set; } = null!;
    public int? Capacity { get; set; }
    public bool? HasMiniBar { get; set; }
    public string? Notes { get; set; }
    public int? SaleOrderCount { get; set; }
    public int? IssueCount { get; set; } 
}
