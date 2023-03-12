using InvemtoryServiceApp.Core.Models;

namespace InvemtoryServiceApp.Core.Repositories
{
    public interface IInventoryRepository
    {
        Task<List<Inventory>> Add(List<Inventory> items);
        Task<List<Inventory>> Remove(List<Inventory> items);
        
    }
}
