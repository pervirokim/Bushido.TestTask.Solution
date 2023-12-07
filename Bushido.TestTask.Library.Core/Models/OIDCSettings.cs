using Bushido.TestTask.Library.Core.Extensions;
using System.Net.Http.Headers;
using System.Text;

namespace Bushido.TestTask.Library.Core.Models
{
    public static class OIDCSettings
    {
        public const string ClientIdHeader = "Client-Id";
        public const string ClientSecretHeader = "Client-Secret";


        public static string ClientId = "";

        public static string ClientSecret = "";

        public static void SetOIDC(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public static HttpClient GenerateHttpClientWithOIDC()
        {
            return GenerateHttpClientWithOIDC(ClientId, ClientSecret);
        }

        public static HttpClient GenerateHttpClientWithOIDC(string clientId, string clientSecret)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Client-Id", clientId);
            httpClient.DefaultRequestHeaders.Add("Client-Secret", clientSecret);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        public static HttpContent GenerateHttpContentForApi<T>(T contentObj) where T : class
        {
            return new StringContent(contentObj.ToJson(), Encoding.UTF8, "application/json");
        }
    }
}
