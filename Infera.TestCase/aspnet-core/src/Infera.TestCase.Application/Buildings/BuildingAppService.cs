using Infera.TestCase.Issues;
using Infera.TestCase.Permissions;
using Infera.TestCase.Rooms;
using Infera.TestCase.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace Infera.TestCase.Buildings
{
    public class BuildingAppService :
        CrudAppService<
            Building,
            BuildingDto,
            Guid,
            PagedAndSortedResultRequestDto,
            BuildingCreateUpdateDto
            >, IBuildingAppService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IIssueRepository _issueRepository;

        public BuildingAppService(IBuildingRepository repository,
            IRoomRepository roomRepository,
            IWarehouseRepository warehouseRepository,
            IIssueRepository issueRepository
            ) : base(repository)
        {
            _roomRepository = roomRepository;
            _warehouseRepository = warehouseRepository;
            _issueRepository = issueRepository;

            GetPolicyName = TestCasePermissions.Buildings.Default;
            GetListPolicyName = TestCasePermissions.Buildings.Default;
            CreatePolicyName = TestCasePermissions.Buildings.Create;
            UpdatePolicyName = TestCasePermissions.Buildings.Edit;
            DeletePolicyName = TestCasePermissions.Buildings.Create;
        }


        public override async Task<BuildingDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Building> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var issueQueryable = await _issueRepository.GetQueryableAsync();
            var roomQueryable = await _roomRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();


            //Prepare a query to join buildings and authors
            var query = from building in queryable
                        where building.Id == id
                        select new BuildingDto {
                            Id = building.Id,
                            Name = building.Name,
                            No = building.No,
                            Addres = building.Addres,
                            IssueCount = (from issue in issueQueryable where building.Id == issue.BuildingId select issue).Count(),
                            RoomCount = (from room in roomQueryable where building.Id == room.BuildingId select room).Count(),
                            WarehouseCount = (from wh in warehouseQueryable where building.Id == wh.BuildingId select wh).Count(),
                            CreationTime = building.CreationTime,
                            CreatorId = building.CreatorId,
                            LastModificationTime = building.LastModificationTime,
                            LastModifierId = building.LastModifierId,
                        };

            //Execute the query and get the building with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Building), id);
            } 
            return queryResult;
        }

        public async Task<ListResultDto<BuildingLookupDto>> GetBuildingLookupAsync()
        {
            var buildings = await Repository.GetListAsync();

            return new ListResultDto<BuildingLookupDto>(
                ObjectMapper.Map<List<Building>, List<BuildingLookupDto>>(buildings)
            );
        }


        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"building.{nameof(Building.Name)}";
            }

            if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "authorName",
                    "author.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"building.{sorting}";
        }

    }
}
