using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Infera.TestCase.SaleOrders
{
    public interface ISaleOrderAppService: ICrudAppService<
                                                        SaleOrderDto,
                                                        Guid,
                                                        PagedAndSortedResultRequestDto,
                                                        SaleOrderCreateUpdateDto
                                                        >
    {
        Task<ListResultDto<SaleOrderLookupDto>> GetSaleOrderLookupAsync();        
    }
}
