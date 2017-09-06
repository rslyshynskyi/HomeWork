using System.Collections.Generic;
using System.Threading.Tasks;
using MatOrderingService.Domain;
using MatOrderingService.Models;

namespace MatOrderingService.Services.Storage
{
    public interface IOrdersList
    {
        Task<OrderInfo[]> GetAllOrders();
        Task<OrderInfo> Get(int id);
        Task<OrderInfo> Create(NewOrder order);
        Task<OrderInfo> Update(int id, EditOrder order);
        Task<bool> Delete(int id);
        Task<OrdersStatisticItem[]> GetStatistic();
        Task<OrdersStatisticItem[]> GetStatisticDapper();
    }
}
