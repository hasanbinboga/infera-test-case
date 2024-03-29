﻿using Infera.TestCase.BuildingWarehouses;
using Infera.TestCase.Issues;
using Infera.TestCase.Permissions;
using Infera.TestCase.Rooms;
using Infera.TestCase.Warehouses;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace Infera.TestCase.Buildings
{
    public class BuildingAppService :
        CrudAppService<
            Building,
            BuildingDto,
            Guid,
            PagedAndSortedResultRequestDto,
            BuildingCreateUpdateDto
            >, IBuildingAppService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IBuildingWarehouseRepository _buildingWarehouseRepository;
        private readonly IIssueRepository _issueRepository;
        private readonly BuildingManager _buildingManager;

        public BuildingAppService(IBuildingRepository repository,
            IRoomRepository roomRepository,
            IWarehouseRepository warehouseRepository,
            IBuildingWarehouseRepository buildingWarehouseRepository,
            IIssueRepository issueRepository,
            BuildingManager buildingManager
            ) : base(repository)
        {
            _roomRepository = roomRepository;
            _warehouseRepository = warehouseRepository;
            _buildingWarehouseRepository = buildingWarehouseRepository;
            _issueRepository = issueRepository;

            _buildingManager = buildingManager;

            GetPolicyName = TestCasePermissions.Buildings.Default;
            GetListPolicyName = TestCasePermissions.Buildings.Default;
            CreatePolicyName = TestCasePermissions.Buildings.Create;
            UpdatePolicyName = TestCasePermissions.Buildings.Edit;
            DeletePolicyName = TestCasePermissions.Buildings.Create;
        }

        [Authorize(TestCasePermissions.Buildings.Create)]
        public override async Task<BuildingDto> CreateAsync(BuildingCreateUpdateDto input)
        {
            var building = await _buildingManager.CreateAsync(
                                                        input.Name,
                                                        input.No,
                                                        input.Addres
                                                    );

            await Repository.InsertAsync(building);

            return ObjectMapper.Map<Building, BuildingDto>(building);
        }

        public override async Task<BuildingDto> GetAsync(Guid id)
        {
            //Get the IQueryable<Building> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var issueQueryable = await _issueRepository.GetQueryableAsync();
            var roomQueryable = await _roomRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var buildingWarehouseQueryable = await _buildingWarehouseRepository.GetQueryableAsync();


            //Prepare a query to join buildings and authors
            var query = from building in queryable
                        where building.Id == id
                        select new BuildingDto
                        {
                            Id = building.Id,
                            Name = building.Name,
                            No = building.No,
                            Addres = building.Addres,
                            IssueCount = (from issue in issueQueryable where building.Id == issue.BuildingId select issue).Count(),
                            RoomCount = (from room in roomQueryable where building.Id == room.BuildingId select room).Count(),
                            InWarehouseCount = (from wh in warehouseQueryable where building.Id == wh.BuildingId select wh).Count(),
                            OwnWarehouseCount = (from wh in buildingWarehouseQueryable where building.Id == wh.BuildingId select wh).Count(),
                            CreationTime = building.CreationTime,
                            CreatorId = building.CreatorId,
                            LastModificationTime = building.LastModificationTime,
                            LastModifierId = building.LastModifierId,
                        };

            //Execute the query and get the building with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Building), id);
            }
            return queryResult;
        }



        public async Task<ListResultDto<BuildingLookupDto>> GetBuildingLookupAsync()
        {
            var buildings = await Repository.GetListAsync();

            return new ListResultDto<BuildingLookupDto>(
                ObjectMapper.Map<List<Building>, List<BuildingLookupDto>>(buildings)
            );
        }

        public override async Task<PagedResultDto<BuildingDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<Building> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var issueQueryable = await _issueRepository.GetQueryableAsync();
            var roomQueryable = await _roomRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var buildingWarehouseQueryable = await _buildingWarehouseRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from building in queryable
                        select new BuildingDto
                        {
                            Id = building.Id,
                            Name = building.Name,
                            No = building.No,
                            Addres = building.Addres,
                            IssueCount = (from issue in issueQueryable where building.Id == issue.BuildingId select issue).Count(),
                            RoomCount = (from room in roomQueryable where building.Id == room.BuildingId select room).Count(),
                            InWarehouseCount = (from wh in warehouseQueryable where building.Id == wh.BuildingId select wh).Count(),
                            OwnWarehouseCount = (from wh in buildingWarehouseQueryable where building.Id == wh.BuildingId select wh).Count(),
                            CreationTime = building.CreationTime,
                            CreatorId = building.CreatorId,
                            LastModificationTime = building.LastModificationTime,
                            LastModifierId = building.LastModifierId,
                        };

            //Paging
            query = query
                .OrderBy(input.Sorting.IsNullOrEmpty() ? "Id" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BuildingDto objects
            var bookDtos = queryResult.ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<BuildingDto>(
                totalCount,
                bookDtos
            );
        }

        [Authorize(TestCasePermissions.Buildings.GetListOfWarehouseAsync)]
        public async Task<PagedResultDto<BuildingWarehouseDto>> GetListOfWarehouseAsync(BuildingListFilterDto input)
        {
            //Get the IQueryable<Building> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var issueQueryable = await _issueRepository.GetQueryableAsync();
            var roomQueryable = await _roomRepository.GetQueryableAsync();
            var warehouseQueryable = await _warehouseRepository.GetQueryableAsync();
            var buildingWarehouseQueryable = await _buildingWarehouseRepository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from buildingWarehouse in buildingWarehouseQueryable
                        join building in queryable on buildingWarehouse.BuildingId equals building.Id
                        where buildingWarehouse.WarehouseId == input.WarehouseId
                        select new BuildingWarehouseDto
                        {
                            Id = buildingWarehouse.Id,
                            BuildingId = building.Id,
                            WarehouseId = buildingWarehouse.WarehouseId,
                            Name = building.Name,
                            No = building.No,
                            Addres = building.Addres,
                            IssueCount = (from issue in issueQueryable where building.Id == issue.BuildingId select issue).Count(),
                            RoomCount = (from room in roomQueryable where building.Id == room.BuildingId select room).Count(),
                            InWarehouseCount = (from wh in warehouseQueryable where building.Id == wh.BuildingId select wh).Count(),
                            OwnWarehouseCount = (from wh in buildingWarehouseQueryable where building.Id == wh.BuildingId select wh).Count(),
                            CreationTime = building.CreationTime,
                            CreatorId = building.CreatorId,
                            LastModificationTime = building.LastModificationTime,
                            LastModifierId = building.LastModifierId,
                        };

            //Paging
            query = query
                .OrderBy(input.Sorting.IsNullOrEmpty() ? "Id" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of BuildingDto objects
            var dtos = queryResult.ToList();

            //Get the total count with another query
            var totalCount = await _buildingWarehouseRepository.GetCountAsync();

            return new PagedResultDto<BuildingWarehouseDto>(
                totalCount,
                dtos
            );
        }

    }
}
