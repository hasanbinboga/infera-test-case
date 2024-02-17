using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.BuildingWarehouses;

public class BuildingWarehouseDto : AuditedEntityDto<Guid>
{
    public Guid BuildingId { get; set; }
    public Guid WarehouseId { get; set; }
}
