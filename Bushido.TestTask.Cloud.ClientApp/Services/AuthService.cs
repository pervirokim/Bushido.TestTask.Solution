using Bushido.TestTask.Cloud.Authentication.Models;
using Bushido.TestTask.Cloud.Authentication.Models.DBModel;
using Bushido.TestTask.Cloud.ClientApp.Interfaces;
using Bushido.TestTask.Library.Core.Extensions;
using Bushido.TestTask.Library.Core.Models;
using Microsoft.Extensions.Configuration;

namespace Bushido.TestTask.Cloud.ClientApp.Services
{
    public class AuthService : IAuthService
    {
        public JWTtoken Token { get; set; } //temp mapping for test;

        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<JWTtoken> SignIn(SignInModel signInModel)
        {
            await ProcessAuthHttpResponse(signInModel, "signin");
            return Token;
        }

        public async Task<JWTtoken> SignUp(SignUpModel signUpModel)
        {
            await ProcessAuthHttpResponse(signUpModel, "signup");
            return Token;
        }

        private async Task ProcessAuthHttpResponse(object data, string endpoint)
        {
            HttpClient client = OIDCSettings.GenerateHttpClientWithOIDC(); //set oidc

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_configuration.GetValue<string>("AuthenticationApi")}/authentication/{endpoint}");
            request.Content = OIDCSettings.GenerateHttpContentForApi(data);
            var httpResult = await client.SendAsync(request);

            if (httpResult.IsSuccessStatusCode)
            {
                string resultJSON = await httpResult.Content.ReadAsStringAsync();
                Token = resultJSON.CloneStringJson<JWTtoken>(); //temp mapping for test;
            }
        }
    }
}
