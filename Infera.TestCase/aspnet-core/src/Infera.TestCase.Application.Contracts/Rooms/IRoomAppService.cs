using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Infera.TestCase.Rooms
{
    public interface IRoomAppService: ICrudAppService<
                                                        RoomDto,
                                                        Guid,
                                                        PagedAndSortedResultRequestDto,
                                                        RoomCreateUpdateDto
                                                        >
    {
        Task<ListResultDto<RoomLookupDto>> GetRoomLookupAsync(); 
    }
}
