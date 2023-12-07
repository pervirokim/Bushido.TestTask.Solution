using Bushido.TestTask.Cloud.ClientApp.Interfaces;
using Bushido.TestTask.Cloud.CryptoTrading.Models;
using Bushido.TestTask.Library.Core.Models;

namespace Bushido.TestTask.Cloud.ClientApp.Services
{
    public class OrderService : IOrderService
    {

        public List<Order> MyOrders { get; set; } = new List<Order>(); //temp mapping for test;

        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        public OrderService(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        public async Task<bool> Create(Order order)
        {
            HttpClient client = OIDCSettings.GenerateHttpClientWithOIDC(); //set oidc
            order.Name = "Random name";
            order.Datecreated = DateTime.Now;
            order.UserId = _authService.Token.UserId;
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_configuration.GetValue<string>("CryptoApi")}/orders/create");
            request.Content = OIDCSettings.GenerateHttpContentForApi(order);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authService.Token.IdToken}");

            var httpResult = await client.SendAsync(request);

            order.OrderStatus = httpResult.IsSuccessStatusCode ? OrderStatus.Success : OrderStatus.Failed;

            MyOrders.Add(order);//temp mapping for test;
            return httpResult.IsSuccessStatusCode;
        }

    }
}
