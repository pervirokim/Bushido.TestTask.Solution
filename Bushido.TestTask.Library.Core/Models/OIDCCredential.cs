using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Bushido.TestTask.Library.Core.Models
{
    public class OIDCCredential:BaseDBModel
    {
        [JsonProperty("clientsecret")]
        public string? ClientSecret { get; set; }

        [JsonProperty("clientid")]
        public string? ClientId { get; set; }
    }
}