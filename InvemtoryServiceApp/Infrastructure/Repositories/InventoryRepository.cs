using InvemtoryServiceApp.Core.Models;
using InvemtoryServiceApp.Core.Repositories;
using InvemtoryServiceApp.Infrastructure.Data;
using System.Linq;

namespace InvemtoryServiceApp.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {

        private readonly InventoryDBContext _context;

        public InventoryRepository(InventoryDBContext context)
        {
            _context = context;
        }

        public async Task<List<Inventory>> Add(List<Inventory> items)
        {

            List<int> productIds = items.Select(x => x.ProductId).ToList();
            List<Inventory> existingItems = _context.Inventories.Where(i => productIds.Contains(i.ProductId)).ToList();
            List<int> existingProductIds = existingItems.Select(x => x.ProductId).ToList();
            List<Inventory> newItems = items.Where(i => !existingProductIds.Contains(i.ProductId)).ToList();

            existingItems.ForEach(e =>
            {
                e.Qty += items.First(i => i.ProductId == e.ProductId).Qty;
            });

            if (newItems.Any())
            {
                _context.Inventories.AddRange(newItems);
            }


            _context.SaveChanges();


            List<Inventory> result = new List<Inventory>();

            if (existingItems.Any())
            {
                result.AddRange(existingItems);
            }

            if (newItems.Any())
            {
                result.AddRange(newItems);
            }
            return result;

        }

        public async Task<List<Inventory>> Remove(List<Inventory> items)
        {
            List<int> productIds = items.Select(x => x.ProductId).ToList();
            List<Inventory> existingItems = _context.Inventories.Where(i => productIds.Contains(i.ProductId)).ToList();
            List<int> existingProductIds = existingItems.Select(x => x.ProductId).ToList();

            existingItems.ForEach(e =>
            {
                e.Qty -= items.First(i => i.ProductId == e.ProductId).Qty;
            });

            _context.SaveChanges();

            return existingItems;
        }
    }
}
