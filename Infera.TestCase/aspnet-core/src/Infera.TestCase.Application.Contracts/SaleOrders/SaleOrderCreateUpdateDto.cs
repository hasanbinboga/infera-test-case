using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Infera.TestCase.SaleOrders;

public class SaleOrderCreateUpdateDto : EntityDto<Guid>
{

    [Required]
    public Guid RoomId { get; set; }
    [Required]
    public Guid WarehouseInventoryId { get; set; }
    [Required]
    public int Count { get; set; }
    public bool IsCompleted { get; set; }

    [DynamicStringLength(typeof(SaleOrderConsts), nameof(SaleOrderConsts.MaxNotesLength))]
    public string? Notes { get; set; } = null!;

}
