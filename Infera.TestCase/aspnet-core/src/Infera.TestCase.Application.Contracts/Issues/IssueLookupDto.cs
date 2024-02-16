using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Issues;
public class IssueLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!; 
}
