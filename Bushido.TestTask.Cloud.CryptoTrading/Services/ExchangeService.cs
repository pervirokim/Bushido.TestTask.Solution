using Bushido.TestTask.Cloud.CryptoTrading.Interfaces;
using Bushido.TestTask.Cloud.CryptoTrading.Models;

namespace Bushido.TestTask.Cloud.CryptoTrading.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IUserBalanceService _userBalanceService;
        private readonly IOrderManagerService _orderManagerService;
        public ExchangeService(IUserBalanceService userBalanceService, IOrderManagerService orderManagerService)
        {
            _userBalanceService = userBalanceService;
            _orderManagerService = orderManagerService;
        }

        public async Task<bool> Exchange(Order order)
        {
            await _orderManagerService.SetOrderStatus(order, OrderStatus.InProgress); //set status in progress
            try
            {
                bool userHaveNeededQuantity = _userBalanceService.GetBalance(order.UserId).FirstOrDefault(b => b.CryptocurrencyId == order.CryptoCurrencyToSoldId).Quantity < order.Quantity;
                bool userTryToExchangeToAnotherCryptoCurrency = order.CryptoCurrencyToSoldId != order.CryptoCurrencyToBuyId;
                if (!userHaveNeededQuantity && !userTryToExchangeToAnotherCryptoCurrency)
                {
                    await _orderManagerService.SetOrderStatus(order, OrderStatus.Failed);//set status failed
                    return false; //user cannot exchange becouse the issue occured
                }
                else
                {
                    float quatationPrice = GetQuatationPrice(order.CryptoCurrencyToSoldId, order.CryptoCurrencyToBuyId);

                    _userBalanceService.UpdateBalance(order.UserId, order.CryptoCurrencyToSoldId, -order.Quantity);
                    _userBalanceService.UpdateBalance(order.UserId, order.CryptoCurrencyToBuyId, order.Quantity * quatationPrice);
                    await _orderManagerService.SetOrderStatus(order, OrderStatus.Success); //set status success

                    //more real logic here, % etc
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                //some additional logic
            }

        }

        public float GetQuatationPrice(string cryptoCurrencyToSoldId, string cryptoCurrencyToBuyId)
        {
            //here get real cost for exhange
            return (float)new Random().NextDouble();
        }
    }
}
