using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.WarehouseInventories;

public class InventoryDto : AuditedEntityDto<Guid>
{
    public Guid? WarehouseId { get; set; }
    public string WarehouseName { get; set; } = null!;
    public string? WarehouseNo { get; set; }
    public int? WarehouseFloor { get; set; }
    public int? WarehouseCapacity { get; set; }
    public string? WarehouseNotes { get; set; }

    public virtual Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductManicaturer { get; set; } = null!;
    public ProductInventoryType ProductType { get; set; }
    public int ProductSize { get; set; }
    public double SalePrice { get; set; }
    public string? ProductNotes { get; set; }


    public int Count { get; set; }
    public int Capacity { get; set; }
    public string? Notes { get; set; }
    public int SaleCount { get; set; }
    public int IssueCount { get; set; }
}
