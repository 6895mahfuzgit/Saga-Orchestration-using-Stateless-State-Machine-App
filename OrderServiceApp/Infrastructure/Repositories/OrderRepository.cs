using Microsoft.EntityFrameworkCore;
using OrderServiceApp.Core.Repositories;
using OrderServiceApp.Infrastructure.Data;
using ProductServiceApp.Core.Models;

namespace OrderServiceApp.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<bool> Save(Order order)
        {
            _context.orders.Add(order);
            _context.SaveChanges();

            if (order.orderDtls.Any())
            {
                order.orderDtls.ToList().ForEach(x =>
                {
                    x.OrderId = order.Id;
                });

              
            }
            _context.orderDtls.AddRange(order.orderDtls);
            return _context.SaveChanges() > 0;
        }
        public async Task<bool> Delete(int id)
        {
            Order order = await _context.orders.FirstOrDefaultAsync(x=>x.Id==id);

            _context.orders.Remove(order);
            return _context.SaveChanges() > 0;

        }

    }
}
