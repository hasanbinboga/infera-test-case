using System;
using Volo.Abp.Application.Dtos;

namespace Infera.TestCase.Rooms;
public class RoomLookupDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!; 
}
