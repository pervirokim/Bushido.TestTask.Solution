using Bushido.TestTask.Cloud.CryptoTrading.Models;

namespace Bushido.TestTask.Cloud.ClientApp.Interfaces
{
    public interface IOrderService
    {
        List<Order> MyOrders { get; set; }

        Task<bool> Create(Order order);
    }
}