using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Buildings;

[Serializable]
public class BuildingListFilterDto : PagedAndSortedResultRequestDto
{
    public Guid WarehouseId { get; set; }
    
}
