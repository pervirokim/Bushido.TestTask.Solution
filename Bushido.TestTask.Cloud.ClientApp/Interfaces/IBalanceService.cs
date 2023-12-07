using Bushido.TestTask.Cloud.CryptoTrading.Models;

namespace Bushido.TestTask.Cloud.ClientApp.Interfaces
{
    public interface IBalanceService
    {
        List<UserBalance> Balance { get; set; }

        Task<List<UserBalance>> GetBalance();
    }
}