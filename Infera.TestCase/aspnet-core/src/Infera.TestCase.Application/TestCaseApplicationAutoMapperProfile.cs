using AutoMapper;
using Infera.TestCase.Accountings;
using Infera.TestCase.Buildings;
using Infera.TestCase.BuildingWarehouses;
using Infera.TestCase.Issues;
using Infera.TestCase.ProductInventories;
using Infera.TestCase.Rooms;
using Infera.TestCase.WarehouseInventories;
using Infera.TestCase.Warehouses;

namespace Infera.TestCase;

public class TestCaseApplicationAutoMapperProfile : Profile
{
    public TestCaseApplicationAutoMapperProfile()
    {
        CreateMap<Building, BuildingDto>();
        CreateMap<BuildingCreateUpdateDto, Building>();
        CreateMap<Building, BuildingLookupDto>();

        CreateMap<BuildingWarehouse, BuildingWarehouses.BuildingWarehouseDto>();
        CreateMap<BuildingWarehouseCreateUpdateDto, BuildingWarehouse>();

        CreateMap<Room, RoomDto>();
        CreateMap<RoomCreateUpdateDto, Room>();
        CreateMap<Room, RoomLookupDto>();

        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<WarehouseCreateUpdateDto, Warehouse>();
        CreateMap<Warehouse, WarehouseLookupDto>();

        CreateMap<WarehouseInventory, InventoryDto>();
        CreateMap<InventoryCreateUpdateDto, Warehouse>();
        CreateMap<WarehouseInventory, InventoryLookupDto>();

        CreateMap<ProductInventory, ProductInventoryDto>();
        CreateMap<ProductInventoryCreateUpdateDto, ProductInventory>();
        CreateMap<ProductInventory, ProductInventoryLookupDto>();

        CreateMap<Accounting, AccountingDto>();
        CreateMap<AccountingCreateUpdateDto, Accounting>();
        CreateMap<Accounting, AccountingLookupDto>();

        CreateMap<Issue, IssueDto>();
        CreateMap<IssueCreateUpdateDto, Issue>();
        CreateMap<Issue, IssueLookupDto>();


    }
}
