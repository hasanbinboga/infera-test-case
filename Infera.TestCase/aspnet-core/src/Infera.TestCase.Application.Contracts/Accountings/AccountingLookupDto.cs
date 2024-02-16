using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Accountings;
public class AccountingLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!; 
}
