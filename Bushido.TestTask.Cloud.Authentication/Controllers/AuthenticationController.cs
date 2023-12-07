using Bushido.TestTask.Cloud.Authentication.Attributes;
using Bushido.TestTask.Cloud.Authentication.Interfaces;
using Bushido.TestTask.Cloud.Authentication.Models;
using Bushido.TestTask.Cloud.Authentication.Models.DBModel;
using Bushido.TestTask.Library.Core.Controllers;
using Bushido.TestTask.Library.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bushido.TestTask.Cloud.Authentication.Controllers
{
    /// <summary>
    /// Controller for user authentication operations.
    /// </summary>
    [TypeFilter(typeof(RequireOIDC))] // Filter to handle OIDC credentials to authenticate API
    [AllowAnonymous] // Allow sending requests without JWT authorization, only OIDC
    public class AuthenticationController : BaseTestTaskController
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="signUpModel">The model containing user registration information.</param>
        /// <returns>The JWT token upon successful registration; otherwise, a bad request.</returns>
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            if (signUpModel.IsEmpty() || signUpModel.Email.IsEmpty() || signUpModel.Password.IsEmpty() || signUpModel.ConfirmPassword.IsEmpty() || !signUpModel.ConfirmPassword.Trim().Equals(signUpModel.Password.Trim()))
                return BadRequest();

            JWTtoken accessToken = await _authService.SignUp(signUpModel);

            return !accessToken.IsEmpty() ? Ok(accessToken) : BadRequest();
        }

        /// <summary>
        /// Authenticates a user based on provided credentials.
        /// </summary>
        /// <param name="signInModel">The model containing user sign-in information.</param>
        /// <returns>The JWT token upon successful authentication; otherwise, a bad request.</returns>
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            if (signInModel.IsEmpty() || signInModel.Email.IsEmpty() || signInModel.Password.IsEmpty())
                return BadRequest();

            JWTtoken accessToken = await _authService.SignIn(signInModel);

            return !accessToken.IsEmpty() ? Ok(accessToken) : BadRequest();
        }

        /// <summary>
        /// Verifies the validity of an access token.
        /// </summary>
        /// <param name="token">The access token to be verified.</param>
        /// <returns>True if the token is valid; otherwise, a bad request.</returns>
        [HttpPost]
        [Route("verifytoken")]
        public IActionResult VerifyToken([FromBody] string token)
        {
            if (token.IsEmpty())
                return BadRequest();

            bool result = _authService.ValidateAccessToken(token);

            return result ? Ok(result) : BadRequest();
        }

        /// <summary>
        /// Refreshes an access token using a refresh token.
        /// </summary>
        /// <param name="token">The refresh token.</param>
        /// <returns>The new JWT token upon successful refresh; otherwise, a bad request.</returns>
        [HttpGet]
        [Route("refreshtoken/{token}")]
        public IActionResult RefreshToken(string token)
        {
            if (token.IsEmpty())
                return BadRequest();

            JWTtoken accessToken = _authService.RefreshToken(token);

            return !accessToken.IsEmpty() ? Ok(accessToken) : BadRequest();
        }
    }
}
