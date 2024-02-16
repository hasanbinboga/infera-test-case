using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Infera.TestCase.Buildings
{
    public interface IBuildingRepository : IRepository<Building, Guid>
    {
        /// <summary>
        /// Find Building by name. Building name is unique. Because of that name value must be controlled before inserting.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Building?> FindByName(string name, CancellationToken cancellationToken = default); 
    }
}
