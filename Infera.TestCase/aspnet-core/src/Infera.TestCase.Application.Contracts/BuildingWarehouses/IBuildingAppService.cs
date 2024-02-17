using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Infera.TestCase.BuildingWarehouses
{
    public interface IBuildingWarehouseAppService : ICrudAppService<
                                                        BuildingWarehouseDto,
                                                        Guid,
                                                        PagedAndSortedResultRequestDto,
                                                        BuildingWarehouseCreateUpdateDto
                                                        >
    {
        
    }
}
