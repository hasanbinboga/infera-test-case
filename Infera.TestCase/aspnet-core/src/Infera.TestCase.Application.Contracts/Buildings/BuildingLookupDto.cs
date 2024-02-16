using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Buildings;
public class BuildingLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!; 
}
