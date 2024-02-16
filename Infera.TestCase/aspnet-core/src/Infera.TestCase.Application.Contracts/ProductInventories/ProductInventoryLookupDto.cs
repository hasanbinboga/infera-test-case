using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.ProductInventories;
public class ProductInventoryLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!; 
}
