using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Buildings;

public class BuildingWarehouseDto: AuditedEntityDto<Guid>
{
    public Guid BuildingId { get; set; }
    public Guid WarehouseId { get; set; }
    public string Name { get; set; } = null!;
    public string No { get; set; } = null!;
    public string? Addres { get; set; }
    public int? RoomCount { get; set; }
    public int? InWarehouseCount { get; set; }
    public int? OwnWarehouseCount { get; set; }
    public int? IssueCount { get; set; } 
}
