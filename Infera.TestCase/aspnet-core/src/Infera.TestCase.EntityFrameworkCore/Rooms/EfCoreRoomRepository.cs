using Infera.TestCase.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Infera.TestCase.Rooms;

internal class EfCoreRoomRepository : EfCoreRepository<TestCaseDbContext, Room, Guid>, IRoomRepository
{
    public EfCoreRoomRepository(IDbContextProvider<TestCaseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    

    public async Task<Room?> FindByBuildingIdAndNo(Guid buildingId, string no, CancellationToken cancellationToken = default)
    {
        var dbSet = await GetQueryableAsync();
        return await dbSet.FirstOrDefaultAsync(s =>
                            s.BuildingId == buildingId && s.No == no,
                            GetCancellationToken(cancellationToken));
    } 
}
