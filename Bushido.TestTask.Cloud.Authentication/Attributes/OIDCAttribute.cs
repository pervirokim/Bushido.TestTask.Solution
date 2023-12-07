
using Bushido.TestTask.Cloud.Authentication.Auth;
using Bushido.TestTask.Cloud.Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace Bushido.TestTask.Cloud.Authentication.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RequireOIDC : ActionFilterAttribute
    {
        private readonly AuthenticationDbContext _authenticationDbContext;

        public RequireOIDC(AuthenticationDbContext authenticationDbContext)
        {
            _authenticationDbContext = authenticationDbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isRequestHaveValidClientIdHeader = context.HttpContext.Request.Headers.Any(h => h.Key.ToLower().Equals(AuthOptions.ClientId.ToLower()) && _authenticationDbContext.OIDCCredentials.Any(c => c.ClientId.ToLower().Equals(h.Value.ToString().ToLower())));
            bool isRequestHaveValidClientSecretHeader = context.HttpContext.Request.Headers.Any(h => h.Key.ToLower().Equals(AuthOptions.ClientSecret.ToLower()) && _authenticationDbContext.OIDCCredentials.Any(c => c.ClientSecret.ToLower().Equals(h.Value.ToString().ToLower())));

            if(!isRequestHaveValidClientIdHeader || !isRequestHaveValidClientSecretHeader)
            {
                context.Result = new BadRequestResult(); 
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
