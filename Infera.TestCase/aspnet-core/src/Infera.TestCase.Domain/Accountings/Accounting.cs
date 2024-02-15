using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
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
        public double Amount { get; set; }
        public double Tax { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string? InvoiceNumber { get; set; }
        public AccountingType Type { get; set; }

        protected Accounting()
        {

        }

        public Accounting(Guid id,
                        [NotNull] Guid productInventoryId,
                        [NotNull] Guid orderId,
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
            SetOrderId(orderId);
            SetCount(count);
            SetType(type);
            PurchasePrice = purchasePrice;
            SalePrice = salesPrice;
            Amount = amount;
            Tax = tax;
            InvoiceDate = invoiceDate;
            SetInvoiceNumber(invoiceNumber);

        }



        internal void SetProductInventoryId(Guid productInventoryId)
        {
            Check.NotNull(productInventoryId, nameof(productInventoryId));
            ProductInventoryId = productInventoryId;
        }

        internal void SetOrderId(Guid orderId)
        {
            Check.NotNull(orderId, nameof(orderId));
            OrderId = orderId;
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
