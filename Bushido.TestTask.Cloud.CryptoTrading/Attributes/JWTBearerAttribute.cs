using Bushido.TestTask.Cloud.Authentication.Auth;
using Bushido.TestTask.Library.Core.Extensions;
using Bushido.TestTask.Library.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bushido.TestTask.Cloud.CryptoTrading.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class JWTBearerAttribute : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        public JWTBearerAttribute(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            string jwtToken = null;
            string bearer = AuthOptions.BearerToken;

            string bearerToken = context.HttpContext.Request.Headers[AuthOptions.AuthorizationHeader];
            if (!bearerToken.IsEmpty() && bearerToken.StartsWith(bearer))
                jwtToken = bearerToken.Substring($"{bearer} ".Length);


            if (jwtToken.IsEmpty())
            {
                BadRequest(context);
                return;
            }
            HttpClient client = OIDCSettings.GenerateHttpClientWithOIDC(); //set oidc

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_configuration.GetValue<string>("AuthenticationApi")}/authentication/verifytoken");
            request.Content = OIDCSettings.GenerateHttpContentForApi(jwtToken);
            var httpResult = client.SendAsync(request).GetAwaiter().GetResult(); //validate user jwt in authentication microservice

            if (!httpResult.IsSuccessStatusCode)
            {
                BadRequest(context);
                return;
            }

            base.OnActionExecuting(context);
        }

        private void BadRequest(ActionExecutingContext context)
        {
            context.Result = new BadRequestResult();
            return;
        }
    }
}
