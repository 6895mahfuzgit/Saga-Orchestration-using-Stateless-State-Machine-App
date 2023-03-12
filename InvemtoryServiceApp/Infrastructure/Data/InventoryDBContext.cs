using InvemtoryServiceApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InvemtoryServiceApp.Infrastructure.Data
{
    public class InventoryDBContext : DbContext
    {
        public InventoryDBContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Inventory> Inventories { get; set; }
    }
}
