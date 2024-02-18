using Infera.TestCase.Buildings;
using Infera.TestCase.BuildingWarehouses;
using Infera.TestCase.Issues;
using Infera.TestCase.Permissions;
using Infera.TestCase.ProductInventories;
using Infera.TestCase.Rooms;
using Infera.TestCase.SaleOrders;
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

namespace Infera.TestCase.Accountings
{
    public class AccountingAppService :
        CrudAppService<
            Accounting,
            AccountingDto,
            Guid,
            PagedAndSortedResultRequestDto,
            AccountingCreateUpdateDto
            >, IAccountingAppService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IBuildingWarehouseRepository _buildingWarehouseRepository;
        private readonly IWarehouseInventoryRepository _warehouseInventoryRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IProductInventoryRepository _productRepository;
        private readonly ISaleOrderRepository _saleOrderRepository;

        public AccountingAppService(IAccountingRepository repository,
            IRoomRepository roomRepository,
            IWarehouseRepository warehouseRepository,
            IBuildingWarehouseRepository buildingWarehouseRepository,
            IBuildingRepository buildingRepository,
            IProductInventoryRepository productRepository,
            ISaleOrderRepository saleOrderRepository,
            IWarehouseInventoryRepository warehouseInventoryRepository
            ) : base(repository)
        {
            _roomRepository = roomRepository;
            _warehouseRepository = warehouseRepository;
            _buildingWarehouseRepository = buildingWarehouseRepository;
            _warehouseInventoryRepository = warehouseInventoryRepository;
            _buildingRepository = buildingRepository;
            _productRepository = productRepository;
            _saleOrderRepository = saleOrderRepository;

            GetPolicyName = TestCasePermissions.Accountings.Default;
            GetListPolicyName = TestCasePermissions.Accountings.Default;
            CreatePolicyName = TestCasePermissions.Accountings.Create;
            UpdatePolicyName = TestCasePermissions.Accountings.Edit;
            DeletePolicyName = TestCasePermissions.Accountings.Create;
        }


        public override async Task<AccountingDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Accounting> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var roomQueryable = await _roomRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var warehouseInventoryQueryable = await _warehouseInventoryRepository.GetQueryableAsync();
            var buildingWarehouseQueryable = await _buildingWarehouseRepository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var productQueryable = await _productRepository.GetQueryableAsync();
            var saleQueryable = await _saleOrderRepository.GetQueryableAsync();


            //Prepare a query to join buildings and authors
            var query = from accounting in queryable
                        where accounting.Id == id
                        join prod in productQueryable on accounting.ProductInventoryId equals prod.Id
                        join s in saleQueryable on accounting.SaleOrderId equals s.Id into sJoin
                        from sale in sJoin.DefaultIfEmpty()
                        join wi in warehouseInventoryQueryable on sale.WarehouseInventoryId equals wi.Id into wiJoin
                        from warehouseInventory in wiJoin.DefaultIfEmpty()
                        join w in warehouseQueryable on warehouseInventory.WarehouseId equals w.Id into wJoin
                        from warehouse in wJoin.DefaultIfEmpty()
                        join r in roomQueryable on sale.RoomId equals r.Id into rJoin
                        from room in rJoin.DefaultIfEmpty()
                        join rb in buildingQueryable on room.BuildingId equals rb.Id into rbJoin
                        from roomBuilding in rbJoin.DefaultIfEmpty()
                        select new AccountingDto {
                            Id = accounting.Id,
                            ProductInventoryId = accounting.ProductInventoryId,
                            ProductName = prod.Name,
                            ProductManifacturer = prod.Manifacturer,
                            ProductSize = prod.Size,
                            ProductType = prod.Type,
                            Amount = accounting.Amount,
                            Count = accounting.Count,
                            InvoiceDate = accounting.InvoiceDate,
                            InvoiceNumber = accounting.InvoiceNumber,
                            SaleOrderId = accounting.SaleOrderId,
                            SalePrice = accounting.SalePrice,
                            IsOrderCompleted = sale.IsCompleted,
                            PurchasePrice = accounting.PurchasePrice,
                            RoomBuildingId = room.BuildingId,
                            RoomFloor = room.Floor,
                            RoomId = room.Id,
                            RoomNo = room.No,
                            RoomBuildingName = roomBuilding.Name,
                            Type = accounting.Type,
                            Tax = accounting.Tax,
                            WarehouseFloor = warehouse.Floor,
                            WarehouseInventoryId = sale.WarehouseInventoryId,
                            WarehouseName = warehouse.Name,
                            WarehouseNo = warehouse.No,
                            CreationTime = accounting.CreationTime,
                            CreatorId = accounting.CreatorId,
                            LastModificationTime = accounting.LastModificationTime,
                            LastModifierId = accounting.LastModifierId,
                        };

            //Execute the query and get the building with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Accounting), id);
            } 
            return queryResult;
        }

        

        public override async Task<PagedResultDto<AccountingDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<Accounting> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var roomQueryable = await _roomRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var warehouseInventoryQueryable = await _warehouseInventoryRepository.GetQueryableAsync();
            var buildingWarehouseQueryable = await _buildingWarehouseRepository.GetQueryableAsync();
            var buildingQueryable = await _buildingRepository.GetQueryableAsync();
            var productQueryable = await _productRepository.GetQueryableAsync();
            var saleQueryable = await _saleOrderRepository.GetQueryableAsync();


            //Prepare a query to join books and authors
            var query = from accounting in queryable 
                        join prod in productQueryable on accounting.ProductInventoryId equals prod.Id
                        join s in saleQueryable on accounting.SaleOrderId equals s.Id into sJoin
                        from sale in sJoin.DefaultIfEmpty()
                        join wi in warehouseInventoryQueryable on sale.WarehouseInventoryId equals wi.Id into wiJoin
                        from warehouseInventory in wiJoin.DefaultIfEmpty()
                        join w in warehouseQueryable on warehouseInventory.WarehouseId equals w.Id into wJoin
                        from warehouse in wJoin.DefaultIfEmpty()
                        join r in roomQueryable on sale.RoomId equals r.Id into rJoin
                        from room in rJoin.DefaultIfEmpty()
                        join rb in buildingQueryable on room.BuildingId equals rb.Id into rbJoin
                        from roomBuilding in rbJoin.DefaultIfEmpty()
                        select new AccountingDto
                        {
                            Id = accounting.Id,
                            ProductInventoryId = accounting.ProductInventoryId,
                            ProductName = prod.Name,
                            ProductManifacturer = prod.Manifacturer,
                            ProductSize = prod.Size,
                            ProductType = prod.Type,
                            Amount = accounting.Amount,
                            Count = accounting.Count,
                            InvoiceDate = accounting.InvoiceDate,
                            InvoiceNumber = accounting.InvoiceNumber,
                            SaleOrderId = accounting.SaleOrderId,
                            SalePrice = accounting.SalePrice,
                            IsOrderCompleted = sale.IsCompleted,
                            PurchasePrice = accounting.PurchasePrice,
                            RoomBuildingId = room.BuildingId,
                            RoomFloor = room.Floor,
                            RoomId = room.Id,
                            RoomNo = room.No,
                            RoomBuildingName = roomBuilding.Name,
                            Type = accounting.Type,
                            Tax = accounting.Tax,
                            WarehouseFloor = warehouse.Floor,
                            WarehouseInventoryId = sale.WarehouseInventoryId,
                            WarehouseName = warehouse.Name,
                            WarehouseNo = warehouse.No,
                            CreationTime = accounting.CreationTime,
                            CreatorId = accounting.CreatorId,
                            LastModificationTime = accounting.LastModificationTime,
                            LastModifierId = accounting.LastModifierId,
                        };

            //Paging
            query = query
                .OrderBy(input.Sorting.IsNullOrEmpty() ? "Id" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of AccountingDto objects
            var bookDtos = queryResult.ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<AccountingDto>(
                totalCount,
                bookDtos
            );
        }

       

    }
}
