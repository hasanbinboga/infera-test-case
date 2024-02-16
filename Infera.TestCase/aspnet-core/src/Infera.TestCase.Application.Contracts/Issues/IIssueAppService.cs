using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Infera.TestCase.Issues;

public interface IIssueAppService: ICrudAppService<
                                                    IssueDto,
                                                    Guid,
                                                    PagedAndSortedResultRequestDto,
                                                    IssueCreateUpdateDto
                                                    >
{
    Task<ListResultDto<IssueLookupDto>> GetIssueLookupAsync();
    Task<ListResultDto<UserLookupDto>> GetUserLookupAsync();
}
