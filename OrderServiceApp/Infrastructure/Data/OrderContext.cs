using Microsoft.EntityFrameworkCore;
using ProductServiceApp.Core.Models;

namespace OrderServiceApp.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDtl> orderDtls { get; set; }
    }
}
