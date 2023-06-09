﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderServiceApp.Core.Repositories;
using ProductServiceApp.Core.Models;

namespace OrderServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        [HttpPost]
        public async Task<Order> Save(Order order)
        {
            return await _orderRepository.Save(order);
        }


        [HttpDelete("{id}")]
        public async Task<Order> Delete(int id)
        {
            return await _orderRepository.Delete(id);
        }

    }
}
