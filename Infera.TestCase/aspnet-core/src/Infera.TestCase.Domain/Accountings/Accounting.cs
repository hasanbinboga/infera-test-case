using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class Accounting : FullAuditedAggregateRoot<Guid>
    {
        public Guid ProductInventoryId { get; set; }
        public Guid OrderId { get; set; }
        public int Count { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public double TotalPrice { get; set; }
        public double Tax { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public AccountingType Type { get; set; }

        public Accounting()
        {
            
        }
    }
}
