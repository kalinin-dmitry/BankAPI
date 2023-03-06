﻿using BankAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BankAPI.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        
        private readonly OrderContextFactory _orderContextFactory;
        public OrdersController(OrderContextFactory orderContextFactory)
        {
            _orderContextFactory = orderContextFactory;
        }

        //POST: api/get-status
        [HttpPost]
        [ActionName("get-status")]
        public async Task<ActionResult<Order>> GetStatus(int id)
        {
            using (var dbContext = _orderContextFactory.Create())
            {
                if (dbContext.Orders == null)
                {
                    return NotFound();
                }
                var order = await dbContext.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }
                switch (order.StatusCode)
                {
                    case 0: return Ok("Статус операции: в обработке");
                    case 1: return Ok("Статус операции: успех");
                    default: return Ok("Статус операции: неуспех");
                }
            }
        }

        //POST: api/create-order
        [HttpPost]
        [ActionName("create-order")]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            using (var dbContext = _orderContextFactory.Create())
            {
                order.StatusCode = 0;
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();
                if (ThreadPool.QueueUserWorkItem(o => UpdateOrderStatus(order.Id))) {
                    return Ok("Операция поставлена в очередь под номером: " + order.Id);
                }
                return NotFound();
            }
        }
        private void UpdateOrderStatus(int id)
        {
            using (var dbContext = _orderContextFactory.Create())
            {
                var order = dbContext.Orders.Find(id);
                Thread.Sleep(new Random().Next(3000, 7000)); // случайная задержка от 3 до 7 секунд
                if (order != null)
                {
                    if (order.PayAmount >= 1000)
                    {
                        order.StatusCode = 1;
                    }
                    else
                    {
                        order.StatusCode = 2;
                    }
                }     

                dbContext.SaveChanges();
            }
        }
    }


}
