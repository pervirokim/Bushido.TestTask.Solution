using Newtonsoft.Json;

namespace Bushido.TestTask.Cloud.Authentication.Models
{
    public class SignUpModel : SignInModel
    {
        [JsonProperty(PropertyName = "confirmpassword")]
        public string ConfirmPassword { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastname")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "isneedtoconfirmpassword")]
        public bool IsNeedToConfirmPassword { get; set; }
    }
}
