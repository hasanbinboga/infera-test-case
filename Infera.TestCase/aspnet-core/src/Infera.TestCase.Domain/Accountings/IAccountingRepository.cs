using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Infera.TestCase.Accountings
{
    public interface IAccountingRepository : IRepository<Accounting, Guid>
    {
        /// <summary>
        /// Find Accounting by invoice data. Accounting invoice data is unique. Because of that name value must be controlled before inserting.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="invoiceNo"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Accounting?> FindByInvoice(DateTime? invoiceDate, string invoiceNo, CancellationToken cancellationToken = default); 
    }
}
