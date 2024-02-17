using Infera.TestCase.Issues;
using Infera.TestCase.Permissions;
using Infera.TestCase.Rooms;
using Infera.TestCase.Warehouses;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Infera.TestCase.BuildingWarehouses
{
    public class BuildingWarehouseAppService :
        CrudAppService<
            BuildingWarehouse,
            BuildingWarehouseDto,
            Guid,
            PagedAndSortedResultRequestDto,
            BuildingWarehouseCreateUpdateDto
            >, IBuildingWarehouseAppService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IBuildingWarehouseRepository _buildingWarehouseRepository;
        private readonly IIssueRepository _issueRepository;
        private readonly BuildingWarehouseManager _buildingManager;

        public BuildingWarehouseAppService(IBuildingWarehouseRepository repository,
            IRoomRepository roomRepository,
            IWarehouseRepository warehouseRepository,
            IBuildingWarehouseRepository buildingWarehouseRepository,
            IIssueRepository issueRepository,
            BuildingWarehouseManager buildingManager
            ) : base(repository)
        {
            _roomRepository = roomRepository;
            _warehouseRepository = warehouseRepository;
            _buildingWarehouseRepository = buildingWarehouseRepository;
            _issueRepository = issueRepository;

            _buildingManager = buildingManager;

            GetPolicyName = TestCasePermissions.BuildingWarehouses.Default;
            GetListPolicyName = TestCasePermissions.BuildingWarehouses.Default;
            CreatePolicyName = TestCasePermissions.BuildingWarehouses.Create;
            UpdatePolicyName = TestCasePermissions.BuildingWarehouses.Edit;
            DeletePolicyName = TestCasePermissions.BuildingWarehouses.Create;
        }

        [Authorize(TestCasePermissions.BuildingWarehouses.Create)]
        public override async Task<BuildingWarehouseDto> CreateAsync(BuildingWarehouseCreateUpdateDto input)
        {
            var building = await _buildingManager.CreateAsync(input.BuildingId, input.WarehouseId);

            await Repository.InsertAsync(building);

            return ObjectMapper.Map<BuildingWarehouse, BuildingWarehouseDto>(building);
        }
         
    }
}
