using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Warehouses;

public class BuildingWarehouseDto: AuditedEntityDto<Guid>
{
    public Guid RelatedBuildingId { get; set; }
    public Guid WarehouseId { get; set; }

    public Guid? BuildingId { get; set; }
    public string? BuildingName { get; set; }
    public string Name { get; set; } = null!;
    public string No { get; set; } = null!;
    public int Floor { get; set; }
    public int? Capacity { get; set; }
    public string? Content { get; set; }
    public string? Notes { get; set; }
    public int? BuildingCount { get; set; }
    public int? InventoryCount { get; set; } 
}
