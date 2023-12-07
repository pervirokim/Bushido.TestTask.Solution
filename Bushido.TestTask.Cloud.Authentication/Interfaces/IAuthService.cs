using Bushido.TestTask.Cloud.Authentication.Models;
using Bushido.TestTask.Cloud.Authentication.Models.DBModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bushido.TestTask.Cloud.Authentication.Interfaces
{
    /// <summary>
    /// Interface for handling user authentication operations.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Generates a JWT token for the specified user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The generated JWT token.</returns>
        JWTtoken GenerateToken(string userId);

        /// <summary>
        /// Retrieves a specific claim from the provided JWT token.
        /// </summary>
        /// <param name="token">The JWT token.</param>
        /// <param name="claimType">The type of the claim to retrieve.</param>
        /// <returns>The requested claim.</returns>
        Claim GetClaim(string token, string claimType);

        /// <summary>
        /// Retrieves the current user based on the HTTP context.
        /// </summary>
        /// <returns>The current user.</returns>
        User? GetCurrentUser();

        /// <summary>
        /// Refreshes an access token using the provided refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>The new JWT token upon successful refresh.</returns>
        JWTtoken RefreshToken(string refreshToken);

        /// <summary>
        /// Authenticates a user based on the provided sign-in model.
        /// </summary>
        /// <param name="signInModel">The model containing user sign-in information.</param>
        /// <returns>The JWT token upon successful authentication.</returns>
        Task<JWTtoken> SignIn(SignInModel signInModel);

        /// <summary>
        /// Registers a new user based on the provided sign-up model.
        /// </summary>
        /// <param name="signUpModel">The model containing user registration information.</param>
        /// <returns>The JWT token upon successful registration.</returns>
        Task<JWTtoken> SignUp(SignUpModel signUpModel);

        /// <summary>
        /// Validates the provided access token.
        /// </summary>
        /// <param name="token">The access token to validate.</param>
        /// <returns>True if the token is valid; otherwise, false.</returns>
        bool ValidateAccessToken(string token);

        /// <summary>
        /// Validates the provided access token and retrieves it.
        /// </summary>
        /// <param name="token">The access token to validate.</param>
        /// <returns>The validated JWT token.</returns>
        JWTtoken ValidateAccessTokenAndGet(string token);
    }
}
