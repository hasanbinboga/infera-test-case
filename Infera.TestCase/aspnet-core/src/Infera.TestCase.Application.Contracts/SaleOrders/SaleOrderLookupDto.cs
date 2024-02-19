using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.SaleOrders;
public class SaleOrderLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!; 
}
