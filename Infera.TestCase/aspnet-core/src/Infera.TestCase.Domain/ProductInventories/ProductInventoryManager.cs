using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Infera.TestCase.ProductInventories;

public class ProductInventoryManager : DomainService
{
    public IProductInventoryRepository ProductInventoryRepository { get; }

    public ProductInventoryManager(IProductInventoryRepository productInventoryRepository)
    {
        ProductInventoryRepository = productInventoryRepository;
    }

    public async Task<ProductInventory> CreateAsync(string name,
                            string manifacturer,
                            ProductInventoryType type,
                            int size,
                            double salePrice,
                            string notes)
    {
        var existingProductInventory= await ProductInventoryRepository.FindProduct(name, manifacturer, size);

        if (existingProductInventory != null)
        {
            throw new BusinessException(TestCaseDomainErrorCodes.ProductInventoryAlreadyExists)
                .WithData("Name", name)
                .WithData("Manifacturer", manifacturer)
                .WithData("Size", size);
        }
        return new ProductInventory(GuidGenerator.Create(), name, manifacturer, type, size, salePrice, notes);
    }
}
