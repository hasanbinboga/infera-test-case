using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Infera.TestCase.Accountings
{
    public class AccountingManager : DomainService
    {
        public IAccountingRepository AccountingRepository { get; }

        public AccountingManager(IAccountingRepository buildingRepository)
        {
            AccountingRepository = buildingRepository; 
        }

        public async Task<Accounting> CreateAsync(Guid productInventoryId,
                        Guid? saleOrderId,
                        int count,
                        AccountingType type,
                        double purchasePrice,
                        double salesPrice,
                        double amount,
                        double tax,
                        DateTime? invoiceDate,
                        string invoiceNumber)
        {
            var existingAccounting = await AccountingRepository.FindByInvoice(invoiceDate, invoiceNumber);

            if (existingAccounting != null)
            {
                //Send data for Localization formatting
                throw new BusinessException(TestCaseDomainErrorCodes.AccountingInvoiceAlreadyExists).WithData("Date", invoiceDate.HasValue?invoiceDate.Value.ToString("dd.MM.yyy"):"").WithData("InvoiceNo", invoiceNumber);
            }
            return new Accounting(GuidGenerator.Create(), productInventoryId, saleOrderId, count, type, purchasePrice, salesPrice, amount, tax, invoiceDate, invoiceNumber); 
        }
    }
}
