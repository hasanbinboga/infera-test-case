using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Infera.TestCase
{
    public class Accounting : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid ProductInventoryId { get; set; }
        public virtual Guid? SaleOrderId { get; set; }
        public virtual int Count { get; set; }
        public virtual double PurchasePrice { get; set; }
        public virtual double SalePrice { get; set; }
        public virtual double Amount { get; set; }
        public virtual double Tax { get; set; }
        public virtual DateTime? InvoiceDate { get; set; }
        public virtual string? InvoiceNumber { get; set; }
        public virtual AccountingType Type { get; set; }

        protected Accounting()
        {

        }

        public Accounting(Guid id,
                        [NotNull] Guid productInventoryId,
                        Guid? saleOrderId,
                        [NotNull] int count,
                        [NotNull] AccountingType type,
                        double purchasePrice,
                        double salesPrice,
                        double amount,
                        double tax,
                        DateTime? invoiceDate, 
                        string invoiceNumber
            ) : base(id)
        {
            SetProductInventoryId(productInventoryId);
            SetCount(count);
            SetType(type);
            PurchasePrice = purchasePrice;
            SalePrice = salesPrice;
            Amount = amount;
            Tax = tax;
            InvoiceDate = invoiceDate;
            SetInvoiceNumber(invoiceNumber);
            SaleOrderId = saleOrderId;

        }



        internal void SetProductInventoryId(Guid productInventoryId)
        {
            Check.NotNull(productInventoryId, nameof(productInventoryId));
            ProductInventoryId = productInventoryId;
        } 

        internal void SetCount(int count)
        {
            Check.NotNull(count, nameof(count));
            Count = count;
        }

        internal void SetType(AccountingType type)
        {
            Check.NotNull(type, nameof(type));
            Type = type;
        }

        internal void SetInvoiceNumber(string invoiceNumber)
        {
            Check.Length(invoiceNumber, nameof(invoiceNumber), AccountingConsts.MaxInvoiceNumberLength);
            InvoiceNumber = invoiceNumber;
        }
    }
}
