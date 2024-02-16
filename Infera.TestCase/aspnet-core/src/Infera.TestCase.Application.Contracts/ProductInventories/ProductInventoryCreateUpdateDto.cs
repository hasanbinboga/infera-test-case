using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Infera.TestCase.ProductInventories;

public class ProductInventoryCreateUpdateDto : EntityDto<Guid>
{
    [Required]
    [DynamicStringLength(typeof(ProductInventoryConsts), nameof(ProductInventoryConsts.MaxNameLength), nameof(ProductInventoryConsts.MinNameLength))]
    public string Name { get; set; } = null!;

    [Required]
    [DynamicStringLength(typeof(ProductInventoryConsts), nameof(ProductInventoryConsts.MaxManifacturerLength))]
    public string Manifacturer { get; set; } = null!;

    [Required]
    public ProductInventoryType Type { get; set; }


    [Required]
    public int Size { get; set; }
    
    [Required]
    public double SalePrice { get; set; }

    [DynamicStringLength(typeof(ProductInventoryConsts), nameof(ProductInventoryConsts.MaxNotesLength))]
    public string? Notes { get; set; } = null!;
}
