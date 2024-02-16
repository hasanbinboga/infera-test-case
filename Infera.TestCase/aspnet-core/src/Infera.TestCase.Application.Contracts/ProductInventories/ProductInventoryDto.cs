using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.ProductInventories;

public class ProductInventoryDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = null!;
    public string Manifacturer { get; set; } = null!;
    public ProductInventoryType Type { get; set; }
    public int Size { get; set; }
    public double SalePrice { get; set; }
    public string? Notes { get; set; }
    public int? WarehouseInventoryCount { get; set; }
    public int? AccountingCount { get; set; }
    public int? IssueCount { get; set; }
}
