using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Infera.TestCase.Buildings
{
    public class BuildingManager : DomainService
    {
        public IBuildingRepository BuildingRepository { get; }

        public BuildingManager(IBuildingRepository buildingRepository)
        {
            BuildingRepository = buildingRepository; 
        }

        public async Task<Building> CreateAsync(string name, string no, string addres)
        {
            var existingBuilding = await BuildingRepository.FindByName(name);

            if (existingBuilding != null)
            {
                //Send data for Localization formatting
                throw new BusinessException(TestCaseDomainErrorCodes.BuildingAlreadyExists).WithData("Name", name);
            }

            return new Building(GuidGenerator.Create(), name, no, addres); 
        }
    }
}
