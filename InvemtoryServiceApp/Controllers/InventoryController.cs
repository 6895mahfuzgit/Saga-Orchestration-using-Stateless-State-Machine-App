using InvemtoryServiceApp.Core.Models;
using InvemtoryServiceApp.Core.Repositories;
using InvemtoryServiceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvemtoryServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpPost("add")]
        public async Task<List<Inventory>> Add(List<Inventory> inventories)
        {
            return await _inventoryRepository.Add(inventories);
        }

        [HttpPost("remove")]
        public async Task<List<Inventory>> Remove(List<Inventory> inventories)
        {
            return await _inventoryRepository.Remove(inventories);
        }
    }
}
