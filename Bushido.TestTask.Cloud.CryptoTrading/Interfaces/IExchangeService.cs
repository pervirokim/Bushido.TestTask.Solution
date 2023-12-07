using Bushido.TestTask.Cloud.CryptoTrading.Models;
using System.Threading.Tasks;

namespace Bushido.TestTask.Cloud.CryptoTrading.Interfaces
{
    /// <summary>
    /// Interface for managing cryptocurrency exchanges.
    /// </summary>
    public interface IExchangeService
    {
        /// <summary>
        /// Executes a cryptocurrency exchange based on the provided order.
        /// </summary>
        /// <param name="order">The order containing exchange details.</param>
        /// <returns>True if the exchange is successful; otherwise, false.</returns>
        Task<bool> Exchange(Order order);

        /// <summary>
        /// Retrieves the quotation price for exchanging the specified cryptocurrencies.
        /// </summary>
        /// <param name="cryptoCurrencyToSoldId">The ID of the cryptocurrency to be sold.</param>
        /// <param name="cryptoCurrencyToBuyId">The ID of the cryptocurrency to be bought.</param>
        /// <returns>The quotation price for the exchange.</returns>
        float GetQuatationPrice(string cryptoCurrencyToSoldId, string cryptoCurrencyToBuyId);
    }
}
