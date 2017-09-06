using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MatOrderingService.Domain;
using MatOrderingService.Models;
using MatOrderingService.Services.CodeGenerator;
using System.Threading;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace MatOrderingService.Services.Storage.Impl
{
    public class OrdersListService: IOrdersList
    {
        private readonly IMapper _mapper;
        private ICodeGenerator _codeGenerator;
        private readonly OrdersDbContext _context;
        
        public OrdersListService(IMapper mapper, ICodeGenerator codeGenerator, OrdersDbContext context)
        {
            _mapper = mapper;
            _codeGenerator = codeGenerator;
            _context = context;
        }

        public async Task<OrderInfo[]> GetAllOrders()
        {
            var orders = await _context.Orders.AsNoTracking().Where(o => !o.IsDeleted)
                .Select(o => _mapper.Map<OrderInfo>(o))
                .ToArrayAsync();

            return orders;
        }

        public async Task<OrderInfo> Get(int id)
        {
            var order = await _context.Orders.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            return _mapper.Map<OrderInfo>(order);
        }
        
        public async Task<OrderInfo> Create(NewOrder order)
        {
            var newOrder = new Order
            {
                CreateDate = DateTime.Now,
                CreatorId = order.CreatorId,
                Status = OrderStatus.New,
                IsDeleted = false,
                OrderItems = new List<OrderItem>()
            };

            newOrder.OrderCode = Guid.NewGuid().ToString();

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            newOrder.OrderCode = await _codeGenerator.GenerateAsync(newOrder.Id);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderInfo>(newOrder);
        }

        public async Task<OrderInfo> Update(int id, EditOrder order)
        {
            var updateOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
            
            if (updateOrder != null)
            {
                //updateOrder.OrderDetails = order.OrderDetails;
            }

            await _context.SaveChangesAsync();
            
            return _mapper.Map<OrderInfo>(updateOrder);
        }

        public async Task<bool> Delete(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order != null && !order.IsDeleted)
            {
                order.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<OrdersStatisticItem[]> GetStatistic()
        {
            var ordersStatisticItems = await _context.Orders
                .AsNoTracking().Where(p => !p.IsDeleted)
                .GroupBy(g => g.CreatorId)
                .Select(p => new OrdersStatisticItem { CreatorId = p.Key, NumberOfOrders = p.Count() })
                .ToArrayAsync();

            return ordersStatisticItems;
        }

        public async Task<OrdersStatisticItem[]> GetStatisticDapper()
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                var ordersStatisticItems = await connection.QueryAsync<OrdersStatisticItem>(@"
                    SELECT CreatorId, COUNT(*) AS NumberOfOrders
                    FROM Orders
                    GROUP BY CreatorId;");

                return ordersStatisticItems.ToArray();
            }
        }
    }
}
