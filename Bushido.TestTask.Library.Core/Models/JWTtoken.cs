using Bushido.TestTask.Library.Core.Models;
using Newtonsoft.Json;

namespace Bushido.TestTask.Cloud.Authentication.Models.DBModel
{
    public class JWTtoken : BaseDBModel
    {
        [JsonProperty("idtoken")]
        public string IdToken { get; set; }

        [JsonProperty("refreshtoken")]
        public string RefreshToken { get; set; }

        [JsonProperty("userid")]
        public string UserId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
