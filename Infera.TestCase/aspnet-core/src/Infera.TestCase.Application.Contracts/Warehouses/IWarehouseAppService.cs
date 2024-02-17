using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Infera.TestCase.Warehouses
{
    public interface IWarehouseAppService: ICrudAppService<
                                                        WarehouseDto,
                                                        Guid,
                                                        PagedAndSortedResultRequestDto,
                                                        WarehouseCreateUpdateDto
                                                        >
    {
        Task<ListResultDto<WarehouseLookupDto>> GetWarehouseLookupAsync();
        Task<PagedResultDto<BuildingWarehouseDto>> GetListOfBuildingAsync(WarehouseListFilterDto input);
    }
}
