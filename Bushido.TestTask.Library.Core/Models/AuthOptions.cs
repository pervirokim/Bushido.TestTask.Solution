using Bushido.TestTask.Library.Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Bushido.TestTask.Cloud.Authentication.Auth
{
    public static class AuthOptions
    {
        public const string AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;
        public const string AuthorizationHeader = "Authorization";
        public const string Token = "token";
        public const string BearerToken = "Bearer";
        public const string ClientId = OIDCSettings.ClientIdHeader;
        public const string ClientSecret = OIDCSettings.ClientSecretHeader;

        //Access token validation properties
        public const string ISSUER = "Bushido.Microservices";
        public const string AUDIENCE = "Bushido.Microservices";
        public const string SCOPE = "Bushido.Microservices";
        public const string AUTHENTICATIONTYPE = "Token";


        public const string IdClaim = "id";
        public const string NameClaim = "name";
        public const string FirstNameClaim = "firstname";
        public const string LastNameClaim = "lastname";
        public const string EmailClaim = "email";

        public const int TokenExpirationInMinutes =30;
    }
}
