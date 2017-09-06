using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MatOrderingService.Domain;
using MatOrderingService.Exceptions;
using MatOrderingService.Models;
using MatOrderingService.Services.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MatOrderingService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrdersController: Controller
    {
        private readonly IOrdersList _ordersList;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersList ordersList, IMapper mapper)
        {
            _ordersList = ordersList;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<OrderInfo[]> Get()
        {
            return await _ordersList.GetAllOrders();
        }

        /// <summary>
        /// Returns orders info by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Order returns succesfully</response>
        /// <response code="404">Order not found</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _ordersList.Get(id);

            if (order == null)
            {
                throw new EntityNotFoundException();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewOrder order)
        {
            if (order == null)
                return new BadRequestResult();

            var newOrder = await _ordersList.Create(order);

            return Ok(newOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EditOrder order)
        {
            var upOrder = await _ordersList.Update(id, order);

            if(upOrder == null)
                return new BadRequestResult();

            return Ok(upOrder);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _ordersList.Delete(id))
            {
                return (IActionResult) Ok("Order was deleted succesfuly");
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("statistic")]
        [ProducesResponseType(typeof(OrdersStatisticItem[]), 200)]
        public async Task<IActionResult> GetStatistic()
        {
            var orderStatisticItems = await _ordersList.GetStatistic();

            return Ok(orderStatisticItems);
        }

        [HttpGet("statistic/dapper")]
        [ProducesResponseType(typeof(OrdersStatisticItem[]), 200)]
        public async Task<IActionResult> GetStatisticDapper()
        {
            var orderStatisticItems = await _ordersList.GetStatisticDapper();

            return Ok(orderStatisticItems);
        }
    }
}
