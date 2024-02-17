using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.BuildingWarehouses;

public class BuildingWarehouseCreateUpdateDto : EntityDto<Guid>
{
    [Required]
    public Guid BuildingId { get; set; }
    [Required]
    public Guid WarehouseId { get; set; }
}
