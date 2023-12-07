using Bushido.TestTask.Cloud.Authentication.Models.DBModel;
using Bushido.TestTask.Library.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Bushido.TestTask.Cloud.Authentication.Services
{
    public class AuthenticationDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
        public DbSet<JWTtoken> AccessTokens { get; set; }
        public DbSet<OIDCCredential> OIDCCredentials { get; set; }

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options, IConfiguration config) : base(options)
        {
            Database.EnsureCreated();
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OIDCCredential>().HasData(new OIDCCredential() //seed default OIDC to API authentication
            {
                Id = Guid.NewGuid().ToString(),
                ClientId = "test-task-client-id",
                ClientSecret = "test-task-client-secret",
            });

            modelBuilder.Entity<User>().HasData(new User() //seed default user for quick testing
            {
                Id = "testuser",
                Firstname = "Zhenia",
                LastName = "Kashperuk",
                LastLoginDate = DateTime.Now,
                Passwordhash = PasswordHasher.HashPassword("Password*8"),
                Email = "test@bushido.com",
            });
        }
    }
}