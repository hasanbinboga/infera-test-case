using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Infera.TestCase.WarehouseInventories
{
    public interface IInventoryAppService: ICrudAppService<
                                                        InventoryDto,
                                                        Guid,
                                                        PagedAndSortedResultRequestDto,
                                                        InventoryCreateUpdateDto
                                                        >
    {
        Task<ListResultDto<InventoryLookupDto>> GetInventoryLookupAsync(); 
    }
}
