using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Accountings;

public class AccountingDto: AuditedEntityDto<Guid>
{
    public Guid ProductInventoryId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductManifacturer { get; set; } = null!;
    public ProductInventoryType ProductType { get; set; } = ProductInventoryType.None!;
    public int ProductSize { get; set; } = 0!;

    public Guid? SaleOrderId { get; set; }
    public Guid? RoomBuildingId { get; set; }
    public string? RoomBuildingName { get; set; }
    public Guid? RoomId { get; set; }
    public int? RoomFloor { get; set; }
    public string? RoomNo { get; set; }
    public Guid? WarehouseInventoryId { get; set; }
    public string? WarehouseName { get; set; } 
    public string? WarehouseNo { get; set; }
    public int? WarehouseFloor { get; set; }
    public bool? IsOrderCompleted { get; set; }

    public int Count { get; set; } = 0!;
    public double PurchasePrice { get; set; } = 0;
    public double SalePrice { get; set; }
    public double Amount { get; set; }
    public double Tax { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public string? InvoiceNumber { get; set; }
    public AccountingType Type { get; set; } = AccountingType.None!;
     
}
