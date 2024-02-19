using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.SaleOrders;

[Serializable]
public class SaleOrderListFilterDto : PagedAndSortedResultRequestDto
{
    public Guid RoomId { get; set; }
    
}
