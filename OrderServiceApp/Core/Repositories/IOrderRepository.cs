using ProductServiceApp.Core.Models;

namespace OrderServiceApp.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> Save(Order order);
        Task<bool> Delete(int id);

    }
}
