using Infera.TestCase.Buildings;
using Infera.TestCase.Issues;
using Infera.TestCase.Permissions;
using Infera.TestCase.ProductInventories;
using Infera.TestCase.Rooms;
using Infera.TestCase.SaleOrders;
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

namespace Infera.TestCase.SaleOrders
{
    public class SaleOrderAppService :
        CrudAppService<
            SaleOrder,
            SaleOrderDto,
            Guid,
            PagedAndSortedResultRequestDto,
            SaleOrderCreateUpdateDto
            >, ISaleOrderAppService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IWarehouseInventoryRepository _inventoryRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IProductInventoryRepository _productRepository;
        private readonly SaleOrderManager _saleOrderManager;

        public SaleOrderAppService(ISaleOrderRepository repository,
            IBuildingRepository buildingRepository,
            IWarehouseInventoryRepository inventoryRepository,
            IWarehouseRepository warehouseRepository,
            IRoomRepository roomRepository,
            IProductInventoryRepository productInventoryRepository,
            SaleOrderManager warehouseManager
            ) : base(repository)
        {
            _buildingRepository = buildingRepository;
            _inventoryRepository = inventoryRepository;
            _warehouseRepository = warehouseRepository;
            _roomRepository = roomRepository;
            _productRepository = productInventoryRepository;
            _saleOrderManager = warehouseManager;

            GetPolicyName = TestCasePermissions.SaleOrders.Default;
            GetListPolicyName = TestCasePermissions.SaleOrders.Default;
            CreatePolicyName = TestCasePermissions.SaleOrders.Create;
            UpdatePolicyName = TestCasePermissions.SaleOrders.Edit;
            DeletePolicyName = TestCasePermissions.SaleOrders.Create;
        }

        [Authorize(TestCasePermissions.SaleOrders.Create)]
        public override async Task<SaleOrderDto> CreateAsync(SaleOrderCreateUpdateDto input)
        {
            var warehouse = await _saleOrderManager.CreateAsync(
                input.RoomId,
                input.WarehouseInventoryId,
                input.Count,
                input.IsCompleted,
                input.Notes
            );

            await Repository.InsertAsync(warehouse);

            var res =  ObjectMapper.Map<SaleOrder, SaleOrderDto>(warehouse);

            return res;
        }

        public override async Task<SaleOrderDto> GetAsync(Guid id)
        {
            //Get the IQueryable<SaleOrder> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var roomQueryable = await _roomRepository.GetQueryableAsync();
            var inventoryQueryable = await _inventoryRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var productQueryable = await _productRepository.GetQueryableAsync();



            //Prepare a query to join books and authors
            var query = from saleOrder in queryable
                        join inventory in inventoryQueryable on saleOrder.WarehouseInventoryId equals inventory.Id
                        join product in productQueryable on inventory.ProductInventoryId equals product.Id
                        join warehouse in warehouseQueryable on inventory.WarehouseId equals warehouse.Id
                        join room in roomQueryable on saleOrder.RoomId equals room.Id
                        join roomBuilding in buildingQueryable on room.BuildingId equals roomBuilding.Id
                        select new SaleOrderDto
                        {
                            Id = saleOrder.Id,
                            IsCompleted = saleOrder.IsCompleted,
                            Count = saleOrder.Count,
                            Notes = saleOrder.Notes,
                            ProductId = product.Id,
                            ProductManicaturer = product.Manifacturer,
                            ProductName = product.Name,
                            SalePrice = product.SalePrice,
                            ProductSize = product.Size,
                            ProductType = product.Type,
                            RoomBuildingId = room.BuildingId,
                            RoomBuildingName = roomBuilding.Name,
                            RoomFloor = room.Floor,
                            RoomId = room.Id,
                            RoomNo = room.No,
                            WarehouseId = warehouse.Id,
                            WarehouseName = warehouse.Name,
                            WarehouseNo = warehouse.No,
                            WarehouseCapacity = warehouse.Capacity,
                            WarehouseFloor = warehouse.Floor,
                            WarehouseInventoryId = inventory.Id,
                            CreationTime = warehouse.CreationTime,
                            CreatorId = warehouse.CreatorId,
                            LastModificationTime = warehouse.LastModificationTime,
                            LastModifierId = warehouse.LastModifierId
                        };

            //Execute the query and get the building with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(SaleOrder), id);
            }
            return queryResult;
        }

        public async Task<ListResultDto<SaleOrderLookupDto>> GetSaleOrderLookupAsync()
        {
            var buildings = await Repository.GetListAsync();

            return new ListResultDto<SaleOrderLookupDto>(
                ObjectMapper.Map<List<SaleOrder>, List<SaleOrderLookupDto>>(buildings)
            );
        }

        public override async Task<PagedResultDto<SaleOrderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<SaleOrder> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var roomQueryable = await _roomRepository.GetQueryableAsync();
            var inventoryQueryable = await _inventoryRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var productQueryable = await _productRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from saleOrder in queryable
                        join inventory in inventoryQueryable on saleOrder.WarehouseInventoryId equals inventory.Id
                        join product in productQueryable on inventory.ProductInventoryId equals product.Id
                        join warehouse in warehouseQueryable on inventory.WarehouseId equals warehouse.Id
                        join room in roomQueryable on saleOrder.RoomId equals room.Id
                        join roomBuilding in buildingQueryable on room.BuildingId equals roomBuilding.Id 
                        select new SaleOrderDto
                        {
                            Id = saleOrder.Id,
                            IsCompleted = saleOrder.IsCompleted,
                            Count = saleOrder.Count,
                            Notes = saleOrder.Notes,
                            ProductId = product.Id,
                            ProductManicaturer = product.Manifacturer,
                            ProductName = product.Name,
                            SalePrice = product.SalePrice,
                            ProductSize = product.Size,
                            ProductType = product.Type,
                            RoomBuildingId = room.BuildingId,
                            RoomBuildingName = roomBuilding.Name,
                            RoomFloor = room.Floor,
                            RoomId = room.Id,
                            RoomNo = room.No,
                            WarehouseId = warehouse.Id,
                            WarehouseName = warehouse.Name,
                            WarehouseNo = warehouse.No,
                            WarehouseCapacity = warehouse.Capacity,
                            WarehouseFloor = warehouse.Floor,
                            WarehouseInventoryId = inventory.Id,
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

            //Convert the query result to a list of SaleOrderDto objects
            var bookDtos = queryResult.ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<SaleOrderDto>(
                totalCount,
                bookDtos
            );
        } 
    }
}
