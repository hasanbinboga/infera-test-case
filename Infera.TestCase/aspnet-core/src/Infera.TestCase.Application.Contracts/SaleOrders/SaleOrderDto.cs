using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.SaleOrders;

public class SaleOrderDto: AuditedEntityDto<Guid>
{
    public Guid RoomId { get; set; }
    public Guid RoomBuildingId { get; set; }
    public string RoomBuildingName { get; set; } = null!;
    public int RoomFloor { get; set; }
    public string RoomNo { get; set; } = null!;
    public Guid WarehouseInventoryId { get; set; }
    public Guid? WarehouseId { get; set; }
    public string WarehouseName { get; set; } = null!;
    public string? WarehouseNo { get; set; }
    public int? WarehouseFloor { get; set; }
    public int? WarehouseCapacity { get; set; } 
    public virtual Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductManicaturer { get; set; } = null!;
    public ProductInventoryType ProductType { get; set; }
    public int ProductSize { get; set; }
    public double SalePrice { get; set; }
    public int Count { get; set; }
    public bool IsCompleted { get; set; }
    public string? Notes { get; set; }
}
