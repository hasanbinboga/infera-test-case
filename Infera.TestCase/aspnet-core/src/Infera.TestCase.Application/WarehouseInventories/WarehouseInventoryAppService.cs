using Infera.TestCase.Buildings;
using Infera.TestCase.Issues;
using Infera.TestCase.Permissions;
using Infera.TestCase.ProductInventories;
using Infera.TestCase.SaleOrders;
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

namespace Infera.TestCase.WarehouseInventories
{
    public class WarehouseInventoryAppService :
        CrudAppService<
            WarehouseInventory,
            InventoryDto,
            Guid,
            PagedAndSortedResultRequestDto,
            InventoryCreateUpdateDto
            >, IInventoryAppService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IProductInventoryRepository _productRepository;
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IIssueRepository _issueRepository;
        private readonly WarehouseInventoryManager _inventoryManager;


        public WarehouseInventoryAppService(IWarehouseInventoryRepository repository,
            IBuildingRepository buildingRepository,
            IProductInventoryRepository productInventoryRepository,
            IWarehouseRepository warehouseRepository,
            ISaleOrderRepository saleRepository,
            IIssueRepository issueRepository,
            WarehouseInventoryManager inventoryManager
            ) : base(repository)
        {
            _buildingRepository = buildingRepository;
            _productRepository = productInventoryRepository;
            _warehouseRepository = warehouseRepository;
            _issueRepository = issueRepository;
            _saleOrderRepository = saleRepository;
            _inventoryManager = inventoryManager;

            GetPolicyName = TestCasePermissions.WarehouseInventories.Default;
            GetListPolicyName = TestCasePermissions.WarehouseInventories.Default;
            CreatePolicyName = TestCasePermissions.WarehouseInventories.Create;
            UpdatePolicyName = TestCasePermissions.WarehouseInventories.Edit;
            DeletePolicyName = TestCasePermissions.WarehouseInventories.Create;
        }

        [Authorize(TestCasePermissions.Warehouses.Create)]
        public override async Task<InventoryDto> CreateAsync(InventoryCreateUpdateDto input)
        {
            var inventory = await _inventoryManager.CreateAsync(
                input.WarehouseId,
                input.ProductId,
                input.Count,
                input.Capacity,
                input.Notes
            );

            await Repository.InsertAsync(inventory);

            var res = ObjectMapper.Map<WarehouseInventory, InventoryDto>(inventory);

            return res;
        }

        public override async Task<InventoryDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Warehouse> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var prodQuaryable = await _productRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var saleQueryable = await _saleOrderRepository.GetQueryableAsync();
            var issueQueryable = await _issueRepository.GetQueryableAsync();


            //Prepare a query to join buildings and authors
            var query = from inventory in queryable
                        join product in prodQuaryable on inventory.ProductInventoryId equals product.Id
                        join warehouse in warehouseQueryable on inventory.WarehouseId equals warehouse.Id
                        join building in buildingQueryable on warehouse.BuildingId equals building.Id into ps
                        from p in ps.DefaultIfEmpty()
                        where inventory.Id == id
                        select new InventoryDto
                        {
                            Id = warehouse.Id,
                            WarehouseId = inventory.WarehouseId,
                            WarehouseCapacity = warehouse.Capacity,
                            WarehouseName = warehouse.Name,
                            WarehouseNo = warehouse.No,
                            WarehouseFloor = warehouse.Floor,
                            WarehouseNotes = warehouse.Notes,
                            Count = inventory.Count,
                            ProductManicaturer = product.Manifacturer,
                            ProductName = product.Name,
                            ProductNotes = product.Notes,
                            ProductType = product.Type,
                            ProductSize = product.Size,
                            SalePrice = product.SalePrice,
                            ProductId = product.Id,
                            Capacity = warehouse.Capacity,
                            Notes = warehouse.Notes,
                            SaleCount = (from s in saleQueryable where s.WarehouseInventoryId == inventory.Id select s).Count(),
                            IssueCount = (from i in issueQueryable where i.WarehouseInventoryId == inventory.Id select i).Count(),
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

        public async Task<ListResultDto<InventoryLookupDto>> GetInventoryLookupAsync()
        {
            //Get the IQueryable<Warehouse> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var prodQuaryable = await _productRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();

            var inventoryList = from inventory in queryable
                                join warehouse in warehouseQueryable on inventory.WarehouseId equals warehouse.Id
                                join prod in prodQuaryable on inventory.ProductInventoryId equals prod.Id
                                select new InventoryLookupDto
                                {
                                    Id = inventory.Id,
                                    Name = $"{warehouse.Name}-{prod.Name} (stock: {inventory.Count})"
                                };


            return new ListResultDto<InventoryLookupDto>(inventoryList.ToList());
        }

        public override async Task<PagedResultDto<InventoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<Warehouse> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var prodQuaryable = await _productRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var saleQueryable = await _saleOrderRepository.GetQueryableAsync();
            var issueQueryable = await _issueRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from inventory in queryable
                        join product in prodQuaryable on inventory.ProductInventoryId equals product.Id
                        join warehouse in warehouseQueryable on inventory.WarehouseId equals warehouse.Id
                        join building in buildingQueryable on warehouse.BuildingId equals building.Id into ps
                        from p in ps.DefaultIfEmpty()
                        select new InventoryDto
                        {
                            Id = warehouse.Id,
                            WarehouseId = inventory.WarehouseId,
                            WarehouseCapacity = warehouse.Capacity,
                            WarehouseName = warehouse.Name,
                            WarehouseNo = warehouse.No,
                            WarehouseFloor = warehouse.Floor,
                            WarehouseNotes = warehouse.Notes,
                            Count = inventory.Count,
                            ProductManicaturer = product.Manifacturer,
                            ProductName = product.Name,
                            ProductNotes = product.Notes,
                            ProductType = product.Type,
                            ProductSize = product.Size,
                            SalePrice = product.SalePrice,
                            ProductId = product.Id,
                            Capacity = warehouse.Capacity,
                            Notes = warehouse.Notes,
                            SaleCount = (from s in saleQueryable where s.WarehouseInventoryId == inventory.Id select s).Count(),
                            IssueCount = (from i in issueQueryable where i.WarehouseInventoryId == inventory.Id select i).Count(),
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

            //Convert the query result to a list of InventoryDto objects
            var bookDtos = queryResult.ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<InventoryDto>(
                totalCount,
                bookDtos
            );
        }

    }
}
