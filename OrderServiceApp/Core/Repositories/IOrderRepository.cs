using ProductServiceApp.Core.Models;

namespace OrderServiceApp.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Save(Order order);
        Task<Order> Delete(int id);

    }
}
