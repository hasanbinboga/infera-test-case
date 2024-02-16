using Infera.TestCase.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Infera.TestCase.Accountings;

internal class EfCoreAccountingRepository : EfCoreRepository<TestCaseDbContext, Accounting, Guid>, IAccountingRepository
{
    public EfCoreAccountingRepository(IDbContextProvider<TestCaseDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<Accounting?> FindByInvoice(DateTime? invoiceDate, string invoiceNo, CancellationToken cancellationToken = default)
    {
        if(invoiceDate == null || invoiceNo.IsNullOrEmpty()) 
        {
            return default;
        }
        var dbSet = await GetQueryableAsync();
        return await dbSet.FirstOrDefaultAsync(s => 
                            s.InvoiceNumber == invoiceNo &&
                            s.InvoiceDate == invoiceDate, 
                            GetCancellationToken(cancellationToken));
    }
}
