using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Infera.TestCase.ProductInventories;

public interface IProductInventoryAppService: ICrudAppService<
                                                    ProductInventoryDto,
                                                    Guid,
                                                    PagedAndSortedResultRequestDto,
                                                    ProductInventoryCreateUpdateDto
                                                    >
{
    Task<ListResultDto<ProductInventoryLookupDto>> GetProductInventoryLookupAsync(); 
}
