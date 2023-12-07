using Bushido.TestTask.Cloud.CryptoTrading.Interfaces;
using Bushido.TestTask.Cloud.CryptoTrading.Models;

namespace Bushido.TestTask.Cloud.CryptoTrading.Services
{
    public class OrderManagerService : IOrderManagerService
    {
        private readonly IOrderRepositoryService _orderRepositoryService;

        public OrderManagerService(IOrderRepositoryService orderRepositoryService)
        {
            _orderRepositoryService = orderRepositoryService;
        }


        public async Task<bool> Create(Order order)
        {
            order.OrderStatus = OrderStatus.Pending;
            return await _orderRepositoryService.PostOrder(order) != null;
        }

        public async Task<bool> SetOrderStatus(Order order, OrderStatus status)
        {
            bool result = false;

            order.OrderStatus = status;
            await _orderRepositoryService.PutOrder(order.Id, order);
            result = true;

            return result;
        }

    }
}
