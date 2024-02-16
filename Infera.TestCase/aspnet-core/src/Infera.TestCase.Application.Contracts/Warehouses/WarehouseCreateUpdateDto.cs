using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Infera.TestCase.Warehouses;

public class WarehouseCreateUpdateDto : EntityDto<Guid>
{

    public Guid BuildingId { get; set; }

    [Required]
    [DynamicStringLength(typeof(WarehouseConsts), nameof(WarehouseConsts.MaxNoLength), nameof(WarehouseConsts.MaxNoLength))]
    public string No { get; set; } = null!;

    [Required]
    public int Floor { get; set; }
    
    [Required]
    public int Capacity { get; set; }

    [DynamicStringLength(typeof(WarehouseConsts), nameof(WarehouseConsts.MaxContentLength))]
    public string? Content { get; set; } = null!;

    [DynamicStringLength(typeof(WarehouseConsts), nameof(WarehouseConsts.MaxNotesLength))]
    public string? Notes { get; set; } = null!;
}
