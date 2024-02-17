using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Warehouses;

[Serializable]
public class WarehouseListFilterDto : PagedAndSortedResultRequestDto
{
    public Guid WarehouseId { get; set; }
    
}
