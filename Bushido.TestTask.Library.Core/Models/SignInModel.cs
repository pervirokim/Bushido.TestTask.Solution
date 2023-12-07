using Newtonsoft.Json;

namespace Bushido.TestTask.Cloud.Authentication.Models
{
    public class SignInModel
    {
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
