using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Infera.TestCase.Issues;

public class IssueCreateUpdateDto : EntityDto<Guid>
{
    public Guid? BuildingId { get; set; }
    public Guid? RoomId { get; set; }
    public Guid? WarehouseInventoryId { get; set; }
    public Guid? ProductInventoryId { get; set; }
    public Guid? Assignee { get; set; }

    public int? Number { get; set; }
    
    [Required]
    public IssueType Type { get; set; }


    [Required]
    public IssueEntityType EntityType { get; set; }


    [DynamicStringLength(typeof(IssueConsts), nameof(IssueConsts.MaxNotesLength))]
    public string? Notes { get; set; }

    public bool? IsCompleted { get; set; }

    public DateTime? CompletedTime { get; set; }

}
