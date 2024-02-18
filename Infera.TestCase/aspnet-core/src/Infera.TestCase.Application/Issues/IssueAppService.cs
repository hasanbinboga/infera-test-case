using Infera.TestCase.Buildings;
using Infera.TestCase.BuildingWarehouses;
using Infera.TestCase.Permissions;
using Infera.TestCase.ProductInventories;
using Infera.TestCase.Rooms;
using Infera.TestCase.WarehouseInventories;
using Infera.TestCase.Warehouses;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Integration;
using Volo.Abp.Users;

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
        private readonly IIdentityUserIntegrationService _identityUserIntegrationService;
        private readonly IssueManager _issueManager;
        private readonly IIdentityUserRepository _identityUserRepository;

        public IssueAppService(IIssueRepository repository,
            IRoomRepository roomRepository,
            IWarehouseRepository warehouseRepository,
            IBuildingWarehouseRepository buildingWarehouseRepository,
            IBuildingRepository buildingRepository,
            IProductInventoryRepository productRepository,
            IIdentityUserIntegrationService identityUserIntegrationService,
            IWarehouseInventoryRepository warehouseInventoryRepository,
            IIdentityUserRepository identityUserRepository,
            IssueManager issueManager
            ) : base(repository)
        {
            _roomRepository = roomRepository;
            _warehouseRepository = warehouseRepository;
            _buildingWarehouseRepository = buildingWarehouseRepository;
            _buildingRepository = buildingRepository;
            _productRepository = productRepository;
            _warehouseInventoryRepository = warehouseInventoryRepository;
            _identityUserIntegrationService = identityUserIntegrationService;
            _issueManager = issueManager;
            _identityUserRepository = identityUserRepository;

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
                        select new IssueDto
                        {
                            Id = issue.Id,
                            Notes = issue.Notes,
                            IsCompleted = issue.IsCompleted,
                            ProductInventoryId = issue.ProductInventoryId,
                            CompletedTime = issue.CompletedTime,
                            Type = issue.Type,
                            AssigneeId = issue.Assignee,
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
            var issues = await Repository.GetListAsync();

            return new ListResultDto<IssueLookupDto>(
                ObjectMapper.Map<List<Issue>, List<IssueLookupDto>>(issues)
            );
        }

        public async Task<ListResultDto<UserLookupDto>> GetUserLookupAsync()
        {
            var users = await _identityUserIntegrationService.SearchAsync(new UserLookupSearchInputDto { MaxResultCount = 50 });

            return new ListResultDto<UserLookupDto>(
                users.Items.Select(x => new UserLookupDto { Id = x.Id, Name = x.Name }).ToList()
            );
        }

        [Authorize(TestCasePermissions.Issues.Default)]
        public async Task<PagedResultDto<IssueDto>> GetListByEntityTypeAsync(IssueListFilterDto input)
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
                        where issue.EntityType == input.EntityType /* &&
                              building.Id == input.BuildingId &&
                              room.Id == input.RoomId &&
                              warehouseInventory.Id == input.WarehouseInventoryId &&
                              prod.Id == input.ProductInventoryId*/
                        select new IssueDto
                        {
                            Id = issue.Id,
                            Notes = issue.Notes,
                            IsCompleted = issue.IsCompleted,
                            ProductInventoryId = issue.ProductInventoryId,
                            CompletedTime = issue.CompletedTime,
                            Type = issue.Type,
                            AssigneeId = issue.Assignee,
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
                            RoomBuildingName = roomBuilding.Name,
                            EntityType = issue.EntityType
                        };

            //Get the total count with another query
            var totalCount = query.Count();

            //Paging
            query = query
                .OrderBy(input.Sorting.IsNullOrEmpty() ? "Id" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            foreach (var item in queryResult)
            {
                if (item.AssigneeId.HasValue)
                {
                    var user = await _identityUserRepository.FindAsync(item.AssigneeId.Value, includeDetails: false, cancellationToken: default);
                    if (user != null)
                    {
                        item.AssigneeName = $"{user.Name} ({user.NormalizedUserName} - {user.UserName})";
                    }
                }
            }

            //Convert the query result to a list of IssueDto objects
            var bookDtos = queryResult.ToList();

           

            return new PagedResultDto<IssueDto>(
                totalCount,
                bookDtos
            );
        }


        [Authorize(TestCasePermissions.Buildings.Create)]
        public override async Task<IssueDto> CreateAsync(IssueCreateUpdateDto input)
        {
            var building = await _issueManager.CreateAsync(input.BuildingId, 
                input.RoomId, 
                input.WarehouseInventoryId, 
                input.ProductInventoryId, 
                input.EntityType, 
                input.Type, 
                input.Number, 
                input.IsCompleted, 
                input.CompletedTime != null? input.CompletedTime.DateTime: null, 
                input.Notes, 
                input.Assignee);

            await Repository.InsertAsync(building);

            return ObjectMapper.Map<Issue, IssueDto>(building);
        }
    }
}
