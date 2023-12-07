using Bushido.TestTask.Cloud.CryptoTrading.Models;
using System.Threading.Tasks;

namespace Bushido.TestTask.Cloud.CryptoTrading.Interfaces
{
    /// <summary>
    /// Interface for managing cryptocurrency orders.
    /// </summary>
    public interface IOrderManagerService
    {
        /// <summary>
        /// Creates a new cryptocurrency order.
        /// </summary>
        /// <param name="order">The order to be created.</param>
        /// <returns>True if the order creation is successful; otherwise, false.</returns>
        Task<bool> Create(Order order);

        /// <summary>
        /// Sets the status of a cryptocurrency order.
        /// </summary>
        /// <param name="order">The order to update.</param>
        /// <param name="status">The new status to set.</param>
        /// <returns>True if the order status is successfully updated; otherwise, false.</returns>
        Task<bool> SetOrderStatus(Order order, OrderStatus status);
    }
}
