using Azure.Core;
using Bushido.TestTask.Cloud.ClientApp.Interfaces;
using Bushido.TestTask.Cloud.CryptoTrading.Models;
using Bushido.TestTask.Library.Core.Extensions;
using Bushido.TestTask.Library.Core.Models;
using System.Net.Http;

namespace Bushido.TestTask.Cloud.ClientApp.Services
{
    public class BalanceService : IBalanceService
    {

        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        public BalanceService(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        public List<UserBalance> Balance { get; set; } //temp mapping for test;

        public async Task<List<UserBalance>> GetBalance()
        {
            HttpClient client = OIDCSettings.GenerateHttpClientWithOIDC(); //set oidc
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration.GetValue<string>("CryptoApi")}/balance/getbalance/{_authService.Token.UserId}");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authService.Token.IdToken}");
            var httpResult = await client.SendAsync(request);

            if (httpResult.IsSuccessStatusCode)
            {
                string resultJSON = await httpResult.Content.ReadAsStringAsync();
                Balance = resultJSON.CloneStringJson<List<UserBalance>>(); //temp mapping for test;
            }
            return Balance;
        }


    }
}
