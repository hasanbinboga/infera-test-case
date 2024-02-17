using Infera.TestCase.Buildings;
using Infera.TestCase.BuildingWarehouses;
using Infera.TestCase.Issues;
using Infera.TestCase.Permissions;
using Infera.TestCase.SaleOrders;
using Infera.TestCase.WarehouseInventories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace Infera.TestCase.Warehouses
{
    public class WarehouseAppService :
        CrudAppService<
            Warehouse,
            WarehouseDto,
            Guid,
            PagedAndSortedResultRequestDto,
            WarehouseCreateUpdateDto
            >, IWarehouseAppService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IBuildingWarehouseRepository _buildingWarehouseRepository;
        private readonly IWarehouseInventoryRepository _warehouseInventoryRepository;
        private readonly WarehouseManager _warehouseManager;

        public WarehouseAppService(IWarehouseRepository repository,
            IBuildingRepository buildingRepository,
            IBuildingWarehouseRepository buildingWarehouseRepository,
            IWarehouseInventoryRepository warehouseInventoryRepository,
            WarehouseManager warehouseManager
            ) : base(repository)
        {
            _buildingRepository = buildingRepository;
            _buildingWarehouseRepository = buildingWarehouseRepository;
            _warehouseInventoryRepository = warehouseInventoryRepository;
            _warehouseManager = warehouseManager;

            GetPolicyName = TestCasePermissions.Warehouses.Default;
            GetListPolicyName = TestCasePermissions.Warehouses.Default;
            CreatePolicyName = TestCasePermissions.Warehouses.Create;
            UpdatePolicyName = TestCasePermissions.Warehouses.Edit;
            DeletePolicyName = TestCasePermissions.Warehouses.Create;
        }

        [Authorize(TestCasePermissions.Warehouses.Create)]
        public override async Task<WarehouseDto> CreateAsync(WarehouseCreateUpdateDto input)
        {
            var warehouse = await _warehouseManager.CreateAsync(
                input.BuildingId,
                input.Name,
                input.No,
                input.Floor,
                input.Capacity,
                input.Content,
                input.Notes
            );

            await Repository.InsertAsync(warehouse);

            var res =  ObjectMapper.Map<Warehouse, WarehouseDto>(warehouse);

            return res;
        }

        public override async Task<WarehouseDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Warehouse> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var buildingWarehouseQueryable = await _buildingWarehouseRepository.GetQueryableAsync();
            var warehouseInventoryQueryable = await _warehouseInventoryRepository.GetQueryableAsync();


            //Prepare a query to join buildings and authors
            var query = from warehouse in queryable
                        join building in buildingQueryable on warehouse.BuildingId equals building.Id into ps
                        from p in ps.DefaultIfEmpty()
                        where warehouse.Id == id
                        select new WarehouseDto
                        {
                            Id = warehouse.Id,
                            BuildingId = warehouse.BuildingId,
                            BuildingName = p.Name,
                            No = warehouse.No,
                            Name = warehouse.Name,
                            Floor = warehouse.Floor,
                            Capacity = warehouse.Capacity,
                            Notes = warehouse.Notes,
                            Content = warehouse.Content,
                            BuildingCount = (from bw in buildingWarehouseQueryable where bw.Id == warehouse.BuildingId select bw).Count(),
                            InventoryCount = (from i in warehouseInventoryQueryable where warehouse.Id == i.WarehouseId select i).Count(),
                            CreationTime = warehouse.CreationTime,
                            CreatorId = warehouse.CreatorId,
                            LastModificationTime = warehouse.LastModificationTime,
                            LastModifierId = warehouse.LastModifierId
                        };

            //Execute the query and get the building with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Warehouse), id);
            }
            return queryResult;
        }

        public async Task<ListResultDto<WarehouseLookupDto>> GetWarehouseLookupAsync()
        {
            var buildings = await Repository.GetListAsync();

            return new ListResultDto<WarehouseLookupDto>(
                ObjectMapper.Map<List<Warehouse>, List<WarehouseLookupDto>>(buildings)
            );
        }

        public override async Task<PagedResultDto<WarehouseDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<Warehouse> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var buildingWarehouseQueryable = await _buildingWarehouseRepository.GetQueryableAsync();
            var warehouseInventoryQueryable = await _warehouseInventoryRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from warehouse in queryable
                        join building in buildingQueryable on warehouse.BuildingId equals building.Id into ps
                        from p in ps.DefaultIfEmpty()
                        select new WarehouseDto
                        {
                            Id = warehouse.Id,
                            BuildingId = warehouse.BuildingId,
                            BuildingName = p.Name,
                            Name = warehouse.Name,
                            No = warehouse.No,
                            Floor = warehouse.Floor,
                            Capacity = warehouse.Capacity,
                            Notes = warehouse.Notes,
                            Content = warehouse.Content,
                            BuildingCount = (from bw in buildingWarehouseQueryable where bw.Id == warehouse.BuildingId select bw).Count(),
                            InventoryCount = (from i in warehouseInventoryQueryable where warehouse.Id == i.WarehouseId select i).Count(),
                            CreationTime = warehouse.CreationTime,
                            CreatorId = warehouse.CreatorId,
                            LastModificationTime = warehouse.LastModificationTime,
                            LastModifierId = warehouse.LastModifierId
                        };

            //Paging
            query = query
                .OrderBy(input.Sorting.IsNullOrEmpty() ? "Id" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of WarehouseDto objects
            var bookDtos = queryResult.ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<WarehouseDto>(
                totalCount,
                bookDtos
            );
        }

        [Authorize(TestCasePermissions.Warehouses.GetListOfBuildingAsync)]
        public async Task<PagedResultDto<BuildingWarehouseDto>> GetListOfBuildingAsync(WarehouseListFilterDto input)
        {
            //Get the IQueryable<Warehouse> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var buildingWarehouseQueryable = await _buildingWarehouseRepository.GetQueryableAsync();
            var warehouseInventoryQueryable = await _warehouseInventoryRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from buildingWarehouse in buildingWarehouseQueryable
                        join warehouse in queryable on buildingWarehouse.WarehouseId equals warehouse.Id
                        join building in buildingQueryable on warehouse.BuildingId equals building.Id into ps
                        from p in ps.DefaultIfEmpty()
                        where buildingWarehouse.WarehouseId == input.WarehouseId
                        select new BuildingWarehouseDto
                        {
                            Id = buildingWarehouse.Id,
                            RelatedBuildingId = buildingWarehouse.BuildingId,
                            WarehouseId = buildingWarehouse.WarehouseId,
                            BuildingId = warehouse.BuildingId,
                            BuildingName = p.Name,
                            Name = warehouse.Name,
                            No = warehouse.No,
                            Floor = warehouse.Floor,
                            Capacity = warehouse.Capacity,
                            Notes = warehouse.Notes,
                            Content = warehouse.Content,
                            BuildingCount = (from bw in buildingWarehouseQueryable where bw.Id == warehouse.BuildingId select bw).Count(),
                            InventoryCount = (from i in warehouseInventoryQueryable where warehouse.Id == i.WarehouseId select i).Count(),
                            CreationTime = warehouse.CreationTime,
                            CreatorId = warehouse.CreatorId,
                            LastModificationTime = warehouse.LastModificationTime,
                            LastModifierId = warehouse.LastModifierId
                        };

            //Paging
            query = query
                .OrderBy(input.Sorting.IsNullOrEmpty() ? "Id" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of WarehouseDto objects
            var dtos = queryResult.ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<BuildingWarehouseDto>(
                totalCount,
                dtos
            );
        }
    }
}
