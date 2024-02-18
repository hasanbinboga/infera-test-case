using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Infera.TestCase.Accountings;

public class AccountingCreateUpdateDto : EntityDto<Guid>
{
    [Required]
    public Guid ProductInventoryId { get; set; }
    public Guid? SaleOrderId { get; set; }
    
    [Required]
    public int Count { get; set; }

    [Required]
    public double PurchasePrice { get; set; }

    [Required]
    public double SalePrice { get; set; }

    [Required]
    public double Amount { get; set; }

    [Required]
    public double Tax { get; set; }

    public Date? InvoiceDate { get; set; }

    [DynamicStringLength(typeof(AccountingConsts), nameof(AccountingConsts.MaxInvoiceNumberLength))]
    public string? InvoiceNumber { get; set; }

    [Required]
    public AccountingType Type { get; set; }
}


