using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Buildings;

public class BuildingDto: AuditedEntityDto<Guid>
{

    public string Name { get; set; } = null!;
    public string No { get; set; } = null!;
    public string? Addres { get; set; }
    public int? RoomCount { get; set; }
    public int? WarehouseCount { get; set; }
    public int? IssueCount { get; set; } 
}
