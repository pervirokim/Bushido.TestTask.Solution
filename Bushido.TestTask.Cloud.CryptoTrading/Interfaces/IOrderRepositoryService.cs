using Bushido.TestTask.Cloud.CryptoTrading.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bushido.TestTask.Cloud.CryptoTrading.Interfaces
{
    /// <summary>
    /// Interface for managing cryptocurrency orders in a repository.
    /// </summary>
    public interface IOrderRepositoryService
    {
        /// <summary>
        /// Deletes a cryptocurrency order based on its ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>True if the order deletion is successful; otherwise, false.</returns>
        Task<bool> DeleteOrder(string id);

        /// <summary>
        /// Retrieves a cryptocurrency order based on its ID.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>The requested order.</returns>
        Task<Order> GetOrder(string id);

        /// <summary>
        /// Retrieves all cryptocurrency orders.
        /// </summary>
        /// <returns>The list of all orders.</returns>
        Task<IEnumerable<Order>> GetOrders();

        /// <summary>
        /// Creates a new cryptocurrency order.
        /// </summary>
        /// <param name="order">The order to be created.</param>
        /// <returns>The created order.</returns>
        Task<Order> PostOrder(Order order);

        /// <summary>
        /// Updates a cryptocurrency order based on its ID.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="order">The updated order information.</param>
        /// <returns>True if the order update is successful; otherwise, false.</returns>
        Task<bool> PutOrder(string id, Order order);
    }
}
