using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.WarehouseInventories;

[Serializable]
public class InventoryListFilterDto : PagedAndSortedResultRequestDto
{
    public Guid WarehouseInventoryId { get; set; }
    public Guid ProductInventoryId { get; set; }
    
}
