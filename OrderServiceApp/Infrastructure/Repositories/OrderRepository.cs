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

        public async Task<Order> Save(Order order)
        {
            try
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
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return order;
        }
        public async Task<Order> Delete(int id)
        {
            Order order = await _context.orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order != null)
            {
                _context.orders.Remove(order);
            }
            _context.SaveChanges();

            return order;

        }

    }
}
