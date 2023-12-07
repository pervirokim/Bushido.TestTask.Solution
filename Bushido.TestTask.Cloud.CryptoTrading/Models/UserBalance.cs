using Bushido.TestTask.Library.Core.Models;
using Newtonsoft.Json;

namespace Bushido.TestTask.Cloud.CryptoTrading.Models
{
    public class UserBalance  : BaseDBModel
    {
        [JsonProperty("cryptocurrency")]
        public Cryptocurrency? Cryptocurrency { get; set; }
        
        [JsonProperty("cryptocurrencyid")]
        public string? CryptocurrencyId { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }

        [JsonProperty("userid")]
        public string UserId { get; set; }
    }
}
