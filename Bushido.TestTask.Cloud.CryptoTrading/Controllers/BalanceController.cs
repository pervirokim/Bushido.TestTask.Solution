using Bushido.TestTask.Cloud.CryptoTrading.Attributes;
using Bushido.TestTask.Cloud.CryptoTrading.Interfaces;
using Bushido.TestTask.Library.Core.Controllers;
using Bushido.TestTask.Library.Core.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Bushido.TestTask.Cloud.CryptoTrading.Controllers
{
    /// <summary>
    /// Controller for managing user cryptocurrency balances.
    /// </summary>
    public class BalanceController : BaseTestTaskController
    {
        private readonly IUserBalanceService _userBalanceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalanceController"/> class.
        /// </summary>
        /// <param name="userBalanceService">The service for managing user cryptocurrency balances.</param>
        public BalanceController(IUserBalanceService userBalanceService)
        {
            _userBalanceService = userBalanceService;
        }

        /// <summary>
        /// Retrieves the cryptocurrency balances of a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The cryptocurrency balances of the user.</returns>
        [HttpGet]
        [Route("getbalance/{userId}")]
        [TypeFilter(typeof(JWTBearerAttribute))]
        public async Task<IActionResult> GetBalance(string userId)
        {
            if (userId.IsEmpty())
                return BadRequest();

            return Ok(_userBalanceService.GetBalance(userId));
        }
    }
}
