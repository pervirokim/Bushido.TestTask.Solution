using Bushido.TestTask.Cloud.CryptoTrading.Interfaces;
using Bushido.TestTask.Cloud.CryptoTrading.Models;
using Bushido.TestTask.Library.Core.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System.Collections.Generic;

namespace Bushido.TestTask.Cloud.CryptoTrading.Services
{
    public class UserBalanceService : IUserBalanceService
    {
        private readonly CryptoTradingDbContext _context;
        public UserBalanceService(CryptoTradingDbContext context)
        {
            _context = context;
        }

        public void UpdateBalance(string userId, string cryptocurrencyId, float quantity)
        {
            UserBalance userBalance = _context.UserBalance.FirstOrDefault(ub => ub.UserId == userId && ub.CryptocurrencyId == cryptocurrencyId);

            if (userBalance == null)
                userBalance = CreateBalance(userId, cryptocurrencyId, quantity);
            else
            {
                userBalance.Quantity += quantity;
                _context.Entry(userBalance).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public List<UserBalance> GetBalance(string userId)
        {
            return _context.UserBalance.Where(ub => ub.UserId == userId).Include(ub=>ub.Cryptocurrency).ToList();
        }


        private UserBalance CreateBalance(string userId, string cryptocurrencyId, float quantity)
        {
            UserBalance userBalance = new UserBalance()
            {
                Id = Guid.NewGuid().ToString(),
                CryptocurrencyId = cryptocurrencyId,
                Quantity = quantity,
                UserId = userId
            };

            _context.UserBalance.Add(userBalance);
            _context.SaveChanges();

            return userBalance;
        }
    }
}
