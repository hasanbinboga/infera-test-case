using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Warehouses;
public class WarehouseLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!; 
}
