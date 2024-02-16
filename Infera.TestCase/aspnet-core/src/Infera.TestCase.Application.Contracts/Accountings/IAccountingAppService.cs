using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Infera.TestCase.Accountings
{
    public interface IAccountingAppService: ICrudAppService<
                                                        AccountingDto,
                                                        Guid,
                                                        PagedAndSortedResultRequestDto,
                                                        AccountingCreateUpdateDto
                                                        >
    {

    }
}
