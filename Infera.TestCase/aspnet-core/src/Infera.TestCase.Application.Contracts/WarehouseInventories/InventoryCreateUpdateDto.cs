using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Infera.TestCase.WarehouseInventories;

public class InventoryCreateUpdateDto : EntityDto<Guid>
{
    [Required]
    public Guid WarehouseId { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public int Count { get; set; }
    [Required]
    public int Capacity { get; set; }

    [DynamicStringLength(typeof(WarehouseConsts), nameof(WarehouseInventoryConsts.MaxNotesLength))]
    public string? Notes { get; set; } = null!; 
   
}
