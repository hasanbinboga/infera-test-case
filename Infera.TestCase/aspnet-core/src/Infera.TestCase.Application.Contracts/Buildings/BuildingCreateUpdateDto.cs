using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;

namespace Infera.TestCase.Buildings
{
    public class BuildingCreateUpdateDto : EntityDto<Guid>
    {
        [Required]
        [DynamicStringLength(typeof(BuildingConsts), nameof(BuildingConsts.MaxNameLength), nameof(BuildingConsts.MinNameLength))]
        public string Name { get; set; } = null!;

        [Required]
        [DynamicStringLength(typeof(BuildingConsts), nameof(BuildingConsts.MaxNoLength))]
        public string No { get; set; } = null!;
        
        [DynamicStringLength(typeof(BuildingConsts), nameof(BuildingConsts.MaxAddresLength))]
        public string? Addres { get; set; } 
    }
}
