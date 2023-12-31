﻿using Application.Services;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Web_Api.Hubs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderController(IOrderService orderService , IHubContext<OrderHub> hubContext)
        {
            this.orderService = orderService;
            _hubContext = hubContext;
        }


        [HttpGet]
        [Route("getOrdersByUserId")]
        public async Task<IActionResult> getOrders(string id)
        {
            var orders= await orderService.getAllByUserId(id);
            if (orders!=null)
                return Ok(orders);
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("getOrderById")]
        public async Task<IActionResult> getById(int id)
        {
            var order = await orderService.GetByIdAsync(id);
            if (order != null)
                return Ok(order);
            else
                return NotFound();
        }


        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> Create(OrderDTO orderDTO)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNewOrderNotification", orderDTO.Id);
            var order = await orderService.Create(orderDTO);
            if (order != null)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveNewOrderNotification", orderDTO.Id);
                Debug.WriteLine("success");
                return Ok(order);
            }
            else
            {
                Debug.WriteLine("failed");
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> Update(OrderDTO orderDTO,int id)
        {
            var order = await orderService.Update(orderDTO, id);
            if (order != null)
                return Ok(order);
            else
                return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteOrder/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await orderService.Delete(id);
            return order ? Ok(order) : BadRequest();
        }
    }

}
