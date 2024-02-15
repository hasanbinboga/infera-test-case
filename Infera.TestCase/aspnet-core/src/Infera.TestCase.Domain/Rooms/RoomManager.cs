using Infera.TestCase.BuildingWarehouses;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Infera.TestCase.Rooms;

public class RoomManager : DomainService
{
    public IRoomRepository RoomRepository { get; }

    public RoomManager(IRoomRepository roomRepository)
    {
        RoomRepository = roomRepository; 
    }

    public async Task<Room> CreateAsync(Guid buildingId,
                    int floor,
                    string no,
                    int? capacity,
                    bool? hasMiniBar,
                    string notes)
    {
        var existingRoom= await RoomRepository.FindByBuildingIdAndNo(buildingId, no);

        if (existingRoom != null)
        {
            throw new BusinessException(TestCaseDomainErrorCodes.RoomAlreadyExists);
        }
        return new Room(GuidGenerator.Create(), buildingId, floor, no, capacity, hasMiniBar, notes);
    }
}
