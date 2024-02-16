using Infera.TestCase.Buildings;
using Infera.TestCase.Issues;
using Infera.TestCase.Permissions;
using Infera.TestCase.SaleOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace Infera.TestCase.Rooms
{
    public class RoomAppService :
        CrudAppService<
            Room,
            RoomDto,
            Guid,
            PagedAndSortedResultRequestDto,
            RoomCreateUpdateDto
            >, IRoomAppService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IIssueRepository _issueRepository;

        public RoomAppService(IRoomRepository repository,
            IBuildingRepository buildingRepository, 
            ISaleOrderRepository saleOrderRepository, 
            IIssueRepository issueRepository
            ) : base(repository)
        {
            _buildingRepository = buildingRepository; 
            _saleOrderRepository = saleOrderRepository; 
            _issueRepository = issueRepository;

            GetPolicyName = TestCasePermissions.Rooms.Default;
            GetListPolicyName = TestCasePermissions.Rooms.Default;
            CreatePolicyName = TestCasePermissions.Rooms.Create;
            UpdatePolicyName = TestCasePermissions.Rooms.Edit;
            DeletePolicyName = TestCasePermissions.Rooms.Create;
        }


        public override async Task<RoomDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Room> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var issueQueryable = await _issueRepository.GetQueryableAsync();
            var saleOrderQueryable = await _saleOrderRepository.GetQueryableAsync(); 


            //Prepare a query to join buildings and authors
            var query = from room in queryable
                        join building in buildingQueryable on room.BuildingId equals building.Id
                        where room.Id == id
                        select new RoomDto {
                            Id = room.Id,
                            BuildingId = room.BuildingId,
                            BuildingName = building.Name,
                            No = room.No,
                            Floor = room.Floor,
                            Capacity = room.Capacity,
                            HasMiniBar = room.HasMiniBar,
                            Notes = room.Notes,
                            IssueCount = (from issue in issueQueryable where room.Id == issue.RoomId select issue).Count(),
                            SaleOrderCount = (from sale in saleOrderQueryable where room.Id == sale.RoomId select sale).Count(),
                            CreationTime = room.CreationTime,
                            CreatorId = room.CreatorId,
                            LastModificationTime = room.LastModificationTime,
                            LastModifierId = room.LastModifierId
                        };

            //Execute the query and get the building with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Room), id);
            } 
            return queryResult;
        }

        public async Task<ListResultDto<RoomLookupDto>> GetRoomLookupAsync()
        {
            var buildings = await Repository.GetListAsync();

            return new ListResultDto<RoomLookupDto>(
                ObjectMapper.Map<List<Room>, List<RoomLookupDto>>(buildings)
            );
        }

        public override async Task<PagedResultDto<RoomDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<Room> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var issueQueryable = await _issueRepository.GetQueryableAsync();
            var saleOrderQueryable = await _saleOrderRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from room in queryable
                        join building in buildingQueryable on room.BuildingId equals building.Id 
                        select new RoomDto
                        {
                            Id = room.Id,
                            BuildingId = room.BuildingId,
                            BuildingName = building.Name,
                            No = room.No,
                            Floor = room.Floor,
                            Capacity = room.Capacity,
                            HasMiniBar = room.HasMiniBar,
                            Notes = room.Notes,
                            IssueCount = (from issue in issueQueryable where room.Id == issue.RoomId select issue).Count(),
                            SaleOrderCount = (from sale in saleOrderQueryable where room.Id == sale.RoomId select sale).Count(),
                            CreationTime = room.CreationTime,
                            CreatorId = room.CreatorId,
                            LastModificationTime = room.LastModificationTime,
                            LastModifierId = room.LastModifierId
                        };

            //Paging
            query = query
                .OrderBy(input.Sorting.IsNullOrEmpty()?"Id": input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of RoomDto objects
            var bookDtos = queryResult.ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<RoomDto>(
                totalCount,
                bookDtos
            );
        }

       

    }
}
