using Bushido.TestTask.Cloud.CryptoTrading.Models;
using Microsoft.EntityFrameworkCore;

namespace Bushido.TestTask.Cloud.CryptoTrading.Services
{
    public class CryptoTradingDbContext : DbContext
    {
      
        public CryptoTradingDbContext(DbContextOptions<CryptoTradingDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Cryptocurrency> Cryptocurrencies { get; set; }
        public DbSet<UserBalance> UserBalance { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string testUserId = "testuser";
            Cryptocurrency bitCoin = new Cryptocurrency("BTC");
            Cryptocurrency bushidoCoin = new Cryptocurrency("BUSHIDO");

            //seed default coins for quickly testing
            modelBuilder.Entity<Cryptocurrency>().HasData(bitCoin, bushidoCoin);


            //seed default test user balance for quickly testing
            modelBuilder.Entity<UserBalance>().HasData(new UserBalance()
            {
                Id = Guid.NewGuid().ToString(),
                CryptocurrencyId = bitCoin.Id,
                Quantity = 300f,
                UserId = testUserId
            },
            new UserBalance()
            {
                Id = Guid.NewGuid().ToString(),
                CryptocurrencyId = bushidoCoin.Id,
                Quantity = 100f,
                UserId = testUserId
            }
            );
        }
    }
}