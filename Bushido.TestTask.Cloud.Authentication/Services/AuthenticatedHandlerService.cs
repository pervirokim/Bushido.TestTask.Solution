using Bushido.TestTask.Cloud.Authentication.Auth;
using Bushido.TestTask.Cloud.Authentication.Models.DBModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Newtonsoft.Json.Linq;
using Bushido.TestTask.Cloud.Authentication.Interfaces;

namespace Bushido.TestTask.Cloud.Authentication.Services
{
    public class AuthenticationHandlerService: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly AuthenticationDbContext _authenticationDbContext;
        private readonly IAuthService _authService;

        public AuthenticationHandlerService(IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger,
            UrlEncoder encoder, 
            ISystemClock clock, 
            AuthenticationDbContext authenticationDbContext,
            IAuthService authService):base(options, logger , encoder, clock)
        {
            _authenticationDbContext = authenticationDbContext;
            _authService = authService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.ContainsKey(AuthOptions.AuthorizationHeader))
                return AuthenticateResult.NoResult();

            string bearerToken = Context.Request.Headers[AuthOptions.AuthorizationHeader];
            if (bearerToken == null || !bearerToken.StartsWith(AuthOptions.BearerToken))
                return AuthenticateResult.Fail("Invalid access token");

            string token = bearerToken.Substring($"{AuthOptions.BearerToken} ".Length);
            try
            {
                if (_authService.ValidateAccessToken(token))
                    return AuthenticateResult.Success(CreateAuthenticationTicket(token));
                else
                    return AuthenticateResult.Fail("Invalid Authentication");
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }

        }
        private AuthenticationTicket CreateAuthenticationTicket(string idToken)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                _authService.GetClaim(idToken, AuthOptions.IdClaim),
                _authService.GetClaim(idToken, AuthOptions.EmailClaim),
                _authService.GetClaim(idToken, AuthOptions.NameClaim),
                _authService.GetClaim(idToken, AuthOptions.FirstNameClaim),
                _authService.GetClaim(idToken, AuthOptions.LastNameClaim),
                }, nameof(ClaimsIdentity))
            });

            return new AuthenticationTicket(claimsPrincipal, JwtBearerDefaults.AuthenticationScheme);
        }


    }
}