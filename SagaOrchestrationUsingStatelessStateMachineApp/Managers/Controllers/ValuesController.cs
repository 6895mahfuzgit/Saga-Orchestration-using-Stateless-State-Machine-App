using Microsoft.AspNetCore.Mvc;
using SagaOrchestrationUsingStatelessStateMachineApp.Proxies.Contracts;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly ITranspostProxy _transpostProxy;
        private readonly IOrderProxy _orderProxy;
        private readonly IInventoryProxy _inventoryProxy;

        public ValuesController(ITranspostProxy transpostProxy, IOrderProxy orderProxy, IInventoryProxy inventoryProxy)
        {
            _transpostProxy = transpostProxy;
            _orderProxy = orderProxy;
            _inventoryProxy = inventoryProxy;
        }

        [HttpGet]
        public async Task<bool> Request()
        {

            //await _transpostProxy.Add(new Models.Transpost
            //{
            //    OrderId = 3,
            //    OrderRefNo = "333",
            //    Status = "a"

            //});

            //await _orderProxy.Save(new Order
            //{
            //    RefNo = "555",
            //    orderDtls = new List<OrderDtl>
            //    {
            //        new OrderDtl{ OrderId=55,
            //        Price=100,
            //        ProductId=1,
            //        ProductName="Pen",
            //        Qty=1
            //        },
            //        new OrderDtl{ OrderId=55,
            //        Price=100,
            //        ProductId=2,
            //        ProductName="Paper",
            //        Qty=3
            //        },
            //    }
            //});

            //await _orderProxy.Delete(3);

            //await _inventoryProxy.Add(new List<Inventory> { 
            // new Inventory
            // {
            //     ProductId= 1,
            //     ProductName= "Pen",
            //     Qty= 1
            // }
            //});

            //await _inventoryProxy.Remove(new List<Inventory> {
            // new Inventory
            // {
            //     ProductId= 2,
            //     ProductName= "Paper",
            //     Qty= 1
            // }
            //});

            return true;
        }
    }
}
