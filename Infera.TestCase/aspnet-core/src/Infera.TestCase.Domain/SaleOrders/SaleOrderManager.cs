using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Infera.TestCase.SaleOrders;

public class SaleOrderManager : DomainService
{
    public ISaleOrderRepository SaleOrderRepository { get; }

    public SaleOrderManager(ISaleOrderRepository roomRepository)
    {
        SaleOrderRepository = roomRepository; 
    }

    public async Task<SaleOrder> CreateAsync(Guid roomId,
                        Guid warehouseInventoryId,
                        int count,
                        bool isCompleted,
                        string? notes)
    {
        return new SaleOrder(GuidGenerator.Create(), roomId, warehouseInventoryId, count, isCompleted,notes);
    }
}
