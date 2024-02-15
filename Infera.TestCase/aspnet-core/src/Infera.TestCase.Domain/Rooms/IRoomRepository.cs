using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Infera.TestCase.Rooms;

public interface IRoomRepository : IBasicRepository<Room, Guid>
{
    Task<Room?> FindByBuildingIdAndNo(Guid buildingId, string no, CancellationToken cancellationToken = default);
}
