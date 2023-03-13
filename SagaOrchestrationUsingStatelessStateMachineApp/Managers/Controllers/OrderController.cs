using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SagaOrchestrationUsingStatelessStateMachineApp.Dtos;
using SagaOrchestrationUsingStatelessStateMachineApp.Managers;
using SagaOrchestrationUsingStatelessStateMachineApp.Models;

namespace SagaOrchestrationUsingStatelessStateMachineApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }


        [HttpPost]
        public async Task<OrderResponse> Post([FromBody] Order order)
        {
            bool response = await _orderManager.CreateOrder(order);
            return new OrderResponse { Success = response };
        }
    }
}
