using Infera.TestCase.Buildings;
using Infera.TestCase.BuildingWarehouses;
using Infera.TestCase.Permissions;
using Infera.TestCase.ProductInventories;
using Infera.TestCase.Rooms;
using Infera.TestCase.WarehouseInventories;
using Infera.TestCase.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace Infera.TestCase.Issues
{
    public class IssueAppService :
        CrudAppService<
            Issue,
            IssueDto,
            Guid,
            PagedAndSortedResultRequestDto,
            IssueCreateUpdateDto
            >, IIssueAppService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IWarehouseInventoryRepository _warehouseInventoryRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IBuildingWarehouseRepository _buildingWarehouseRepository;
        private readonly IProductInventoryRepository _productRepository;

        public IssueAppService(IIssueRepository repository,
            IRoomRepository roomRepository,
            IWarehouseRepository warehouseRepository,
            IBuildingWarehouseRepository buildingWarehouseRepository,
            IBuildingRepository buildingRepository,
            IProductInventoryRepository productRepository,
            IWarehouseInventoryRepository warehouseInventoryRepository
            ) : base(repository)
        {
            _roomRepository = roomRepository;
            _warehouseRepository = warehouseRepository;
            _buildingWarehouseRepository = buildingWarehouseRepository;
            _buildingRepository = buildingRepository;
            _productRepository = productRepository;
            _warehouseInventoryRepository = warehouseInventoryRepository;

            GetPolicyName = TestCasePermissions.Issues.Default;
            GetListPolicyName = TestCasePermissions.Issues.Default;
            CreatePolicyName = TestCasePermissions.Issues.Create;
            UpdatePolicyName = TestCasePermissions.Issues.Edit;
            DeletePolicyName = TestCasePermissions.Issues.Create;
        }


        public override async Task<IssueDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Issue> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var prodQueryable = await _productRepository.GetQueryableAsync();
            var roomQueryable = await _roomRepository.GetQueryableAsync();
            var warehouseInventoryQueryable = await _warehouseInventoryRepository.GetQueryableAsync(); 
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var buildingWarehouseQueryable = await _buildingWarehouseRepository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();


            //Prepare a query to join buildings and authors
            var query = from issue in queryable
                        join b in buildingQueryable on issue.BuildingId equals b.Id into bjoin
                        from building in bjoin.DefaultIfEmpty()
                        join p in prodQueryable on issue.ProductInventoryId equals p.Id into pjoin
                        from prod in pjoin.DefaultIfEmpty()
                        join wi in warehouseInventoryQueryable on issue.WarehouseInventoryId equals wi.Id into wijoin
                        from warehouseInventory in wijoin.DefaultIfEmpty()
                        join w in warehouseQueryable on warehouseInventory.WarehouseId equals w.Id into wjoin
                        from warehouse in wjoin.DefaultIfEmpty()
                        join r in roomQueryable on issue.RoomId equals r.Id into rjoin
                        from room in rjoin.DefaultIfEmpty()
                        join b in buildingQueryable on room.BuildingId equals b.Id into rbjoin
                        from roomBuilding in rbjoin.DefaultIfEmpty()
                        where issue.Id == id
                        select new IssueDto {
                            Id = issue.Id,
                            Notes = issue.Notes,
                            IsCompleted = issue.IsCompleted,
                            ProductInventoryId = issue.ProductInventoryId,
                            CompletedTime = issue.CompletedTime,
                            Type = issue.Type,
                            Assignee = issue.Assignee,
                            BuildingId = issue.BuildingId,
                            BuildingName = building.Name,
                            RoomId = issue.RoomId,
                            WarehouseInventoryId = issue.WarehouseInventoryId,
                            CreationTime = issue.CreationTime,
                            CreatorId = issue.CreatorId,
                            LastModificationTime = issue.LastModificationTime,
                            LastModifierId = issue.LastModifierId,
                            Number = issue.Number,
                            ProductName = prod.Name,
                            WarehouseFloor = warehouse.Floor,
                            WarehouseName = warehouse.Name,
                            WarehouseNo = warehouse.No,
                            RoomCapacity = room.Capacity,
                            RoomNo = room.No,
                            RoomFloor = room.Floor,
                            RoomBuildingId = room.BuildingId,
                            RoomBuildingName = roomBuilding.Name
                        };

            //Execute the query and get the building with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Issue), id);
            } 
            return queryResult;
        }

        public async Task<ListResultDto<IssueLookupDto>> GetIssueLookupAsync()
        {
            var buildings = await Repository.GetListAsync();

            return new ListResultDto<IssueLookupDto>(
                ObjectMapper.Map<List<Issue>, List<IssueLookupDto>>(buildings)
            );
        }

        public override async Task<PagedResultDto<IssueDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<Issue> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var prodQueryable = await _productRepository.GetQueryableAsync();
            var roomQueryable = await _roomRepository.GetQueryableAsync();
            var warehouseInventoryQueryable = await _warehouseInventoryRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var buildingWarehouseQueryable = await _buildingWarehouseRepository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from issue in queryable
                        join b in buildingQueryable on issue.BuildingId equals b.Id into bjoin
                        from building in bjoin.DefaultIfEmpty()
                        join p in prodQueryable on issue.ProductInventoryId equals p.Id into pjoin
                        from prod in pjoin.DefaultIfEmpty()
                        join wi in warehouseInventoryQueryable on issue.WarehouseInventoryId equals wi.Id into wijoin
                        from warehouseInventory in wijoin.DefaultIfEmpty()
                        join w in warehouseQueryable on warehouseInventory.WarehouseId equals w.Id into wjoin
                        from warehouse in wjoin.DefaultIfEmpty()
                        join r in roomQueryable on issue.RoomId equals r.Id into rjoin
                        from room in rjoin.DefaultIfEmpty()
                        join b in buildingQueryable on room.BuildingId equals b.Id into rbjoin
                        from roomBuilding in rbjoin.DefaultIfEmpty() 
                        select new IssueDto
                        {
                            Id = issue.Id,
                            Notes = issue.Notes,
                            IsCompleted = issue.IsCompleted,
                            ProductInventoryId = issue.ProductInventoryId,
                            CompletedTime = issue.CompletedTime,
                            Type = issue.Type,
                            Assignee = issue.Assignee,
                            BuildingId = issue.BuildingId,
                            BuildingName = building.Name,
                            RoomId = issue.RoomId,
                            WarehouseInventoryId = issue.WarehouseInventoryId,
                            CreationTime = issue.CreationTime,
                            CreatorId = issue.CreatorId,
                            LastModificationTime = issue.LastModificationTime,
                            LastModifierId = issue.LastModifierId,
                            Number = issue.Number,
                            ProductName = prod.Name,
                            WarehouseFloor = warehouse.Floor,
                            WarehouseName = warehouse.Name,
                            WarehouseNo = warehouse.No,
                            RoomCapacity = room.Capacity,
                            RoomNo = room.No,
                            RoomFloor = room.Floor,
                            RoomBuildingId = room.BuildingId,
                            RoomBuildingName = roomBuilding.Name
                        };

            //Paging
            query = query
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of IssueDto objects
            var bookDtos = queryResult.ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<IssueDto>(
                totalCount,
                bookDtos
            );
        }

       

    }
}
