using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Infera.TestCase.Rooms;

public class RoomCreateUpdateDto : EntityDto<Guid>
{

    [Required]
    public Guid BuildingId { get; set; }

    [Required]
    [DynamicStringLength(typeof(RoomConsts), nameof(RoomConsts.MaxNoLength))]
    public string No { get; set; } = null!;

    [Required]
    public int Floor { get; set; }
    
    public int? Capacity { get; set; }
    public virtual bool? HasMiniBar { get; set; }

    [DynamicStringLength(typeof(RoomConsts), nameof(RoomConsts.MaxNotesLength))]
    public string? Notes { get; set; } = null!;
}
