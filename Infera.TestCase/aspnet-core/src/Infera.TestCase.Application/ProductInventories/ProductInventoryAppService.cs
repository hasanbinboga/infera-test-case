using Infera.TestCase.Accountings;
using Infera.TestCase.Issues;
using Infera.TestCase.Permissions;
using Infera.TestCase.WarehouseInventories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;

namespace Infera.TestCase.ProductInventories
{
    public class ProductInventoryAppService :
        CrudAppService<
            ProductInventory,
            ProductInventoryDto,
            Guid,
            PagedAndSortedResultRequestDto,
            ProductInventoryCreateUpdateDto
            >, IProductInventoryAppService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IWarehouseInventoryRepository _warehouseInventoryRepository;
        private readonly IAccountingRepository _accountingRepository;

        public ProductInventoryAppService(IProductInventoryRepository repository,
            IAccountingRepository accountingRepository,
            IWarehouseInventoryRepository warehouseInventoryRepository,
            IIssueRepository issueRepository
            ) : base(repository)
        {
            _issueRepository = issueRepository;
            _warehouseInventoryRepository = warehouseInventoryRepository;
            _accountingRepository = accountingRepository;

            GetPolicyName = TestCasePermissions.ProductInventories.Default;
            GetListPolicyName = TestCasePermissions.ProductInventories.Default;
            CreatePolicyName = TestCasePermissions.ProductInventories.Create;
            UpdatePolicyName = TestCasePermissions.ProductInventories.Edit;
            DeletePolicyName = TestCasePermissions.ProductInventories.Create;
        }


        public override async Task<ProductInventoryDto> GetAsync(Guid id)
        {
            //Get the IQueryable<ProductInventory> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var issueQueryable = await _issueRepository.GetQueryableAsync();
            var warehouseInventoryQueryable = await _warehouseInventoryRepository.GetQueryableAsync();
            var accountingQueryable = await _accountingRepository.GetQueryableAsync();


            //Prepare a query to join buildings and authors
            var query = from product in queryable
                        where product.Id == id
                        select new ProductInventoryDto
                        {
                            Id = product.Id,
                            Manifacturer = product.Manifacturer,
                            Name = product.Name,
                            SalePrice = product.SalePrice,
                            Size = product.Size,
                            Type = product.Type,
                            Notes = product.Notes,
                            AccountingCount = (from ac in accountingQueryable where ac.ProductInventoryId == product.Id select ac).Count(),
                            IssueCount = (from i in issueQueryable where product.Id == i.ProductInventoryId select i).Count(),
                            WarehouseInventoryCount = (from i in warehouseInventoryQueryable where product.Id == i.ProductInventoryId select i).Count(),
                            CreationTime = product.CreationTime,
                            CreatorId = product.CreatorId,
                            LastModificationTime = product.LastModificationTime,
                            LastModifierId = product.LastModifierId
                        };

            //Execute the query and get the building with author
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(ProductInventory), id);
            }
            return queryResult;
        }

        public async Task<ListResultDto<ProductInventoryLookupDto>> GetProductInventoryLookupAsync()
        {
            var buildings = await Repository.GetListAsync();

            return new ListResultDto<ProductInventoryLookupDto>(
                ObjectMapper.Map<List<ProductInventory>, List<ProductInventoryLookupDto>>(buildings)
            );
        }

        public override async Task<PagedResultDto<ProductInventoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<ProductInventory> from the repository
            var queryable = await Repository.GetQueryableAsync();
            var issueQueryable = await _issueRepository.GetQueryableAsync();
            var warehouseInventoryQueryable = await _warehouseInventoryRepository.GetQueryableAsync();
            var accountingQueryable = await _accountingRepository.GetQueryableAsync();


            //Prepare a query to join books and authors
            var query = from product in queryable 
                        select new ProductInventoryDto
                        {
                            Id = product.Id,
                            Manifacturer = product.Manifacturer,
                            Name = product.Name,
                            SalePrice = product.SalePrice,
                            Size = product.Size,
                            Type = product.Type,
                            Notes = product.Notes,
                            AccountingCount = (from ac in accountingQueryable where ac.ProductInventoryId == product.Id select ac).Count(),
                            IssueCount = (from i in issueQueryable where product.Id == i.ProductInventoryId select i).Count(),
                            WarehouseInventoryCount = (from i in warehouseInventoryQueryable where product.Id == i.ProductInventoryId select i).Count(),
                            CreationTime = product.CreationTime,
                            CreatorId = product.CreatorId,
                            LastModificationTime = product.LastModificationTime,
                            LastModifierId = product.LastModifierId
                        };

            //Paging
            query = query
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of ProductInventoryDto objects
            var bookDtos = queryResult.ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<ProductInventoryDto>(
                totalCount,
                bookDtos
            );
        }



    }
}
