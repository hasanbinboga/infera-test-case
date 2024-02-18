using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.WarehouseInventories;
public class InventoryLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!; 
}
