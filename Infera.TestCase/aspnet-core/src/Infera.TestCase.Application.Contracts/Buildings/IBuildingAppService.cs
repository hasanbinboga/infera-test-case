using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Infera.TestCase.Buildings
{
    public interface IBuildingAppService: ICrudAppService<
                                                        BuildingDto,
                                                        Guid,
                                                        PagedAndSortedResultRequestDto,
                                                        BuildingCreateUpdateDto
                                                        >
    {
        Task<ListResultDto<BuildingLookupDto>> GetBuildingLookupAsync();
        Task<PagedResultDto<BuildingWarehouseDto>> GetListOfWarehouseAsync(BuildingListFilterDto input);
    }
}
