using Bushido.TestTask.Cloud.CryptoTrading.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bushido.TestTask.Cloud.CryptoTrading.Interfaces
{
    /// <summary>
    /// Interface for managing user cryptocurrency balances.
    /// </summary>
    public interface IUserBalanceService
    {
        /// <summary>
        /// Retrieves the cryptocurrency balances of a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of user balances.</returns>
        List<UserBalance> GetBalance(string userId);

        /// <summary>
        /// Updates the cryptocurrency balance of a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="cryptocurrencyId">The ID of the cryptocurrency.</param>
        /// <param name="quantity">The new quantity of the cryptocurrency for the user.</param>
        void UpdateBalance(string userId, string cryptocurrencyId, float quantity);
    }
}
