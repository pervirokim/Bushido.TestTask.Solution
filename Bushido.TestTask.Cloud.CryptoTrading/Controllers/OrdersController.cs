using Bushido.TestTask.Cloud.CryptoTrading.Attributes;
using Bushido.TestTask.Cloud.CryptoTrading.Interfaces;
using Bushido.TestTask.Cloud.CryptoTrading.Models;
using Bushido.TestTask.Library.Core.Controllers;
using Bushido.TestTask.Library.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bushido.TestTask.Cloud.CryptoTrading.Controllers
{
    /// <summary>
    /// Controller for managing cryptocurrency orders.
    /// </summary>
    public class OrdersController : BaseTestTaskController
    {
        private readonly IOrderManagerService _orderManagerService;
        private readonly IExchangeService _exchangeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="orderManagerService">The service for managing cryptocurrency orders.</param>
        public OrdersController(IOrderManagerService orderManagerService, IExchangeService exchangeService)
        {
            _orderManagerService = orderManagerService;
            _exchangeService = exchangeService;
        }

        /// <summary>
        /// Creates a new cryptocurrency order.
        /// </summary>
        /// <param name="order">The model containing information about the order.</param>
        /// <returns>Ok if the order creation is successful; otherwise, a bad request.</returns>
        [HttpPost]
        [Route("create")]
        [TypeFilter(typeof(JWTBearerAttribute))]
        [AllowAnonymous]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order.IsEmpty() || order.CryptoCurrencyToBuyId.IsEmpty() ||
                order.CryptoCurrencyToSoldId.IsEmpty() ||
                order.Quantity.IsEmpty() ||
                order.Quantity<=0
                )
                return BadRequest();

            bool orderCreationResult = await _orderManagerService.Create(order);
            bool result = false;
            if (orderCreationResult)
                result = await _exchangeService.Exchange(order);

            return result ? Ok() : BadRequest();
        }
    }
}
