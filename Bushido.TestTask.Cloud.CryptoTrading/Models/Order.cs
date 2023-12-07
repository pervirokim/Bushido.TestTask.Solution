using Bushido.TestTask.Library.Core.Models;
using Newtonsoft.Json;

namespace Bushido.TestTask.Cloud.CryptoTrading.Models
{
    public class Order: BaseDBModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cryptocurrencytosoldid")]
        public string? CryptoCurrencyToSoldId { get; set; }

        [JsonProperty("cryptocurrencytosold")]
        public Cryptocurrency? CryptoCurrencyToSold { get; set; }

        [JsonProperty("cryptocurrencytobuyid")]
        public string? CryptoCurrencyToBuyId { get; set; }

        [JsonProperty("cryptocurrencytobuy")]
        public Cryptocurrency? CryptoCurrencyToBuy { get; set; }

        [JsonProperty("userid")]
        public string UserId { get; set; }

        [JsonProperty("datecreated")]
        public DateTime Datecreated { get; set; }

        [JsonProperty("orderstatus")]
        public OrderStatus OrderStatus { get; set; }

        [JsonProperty("quantity")]
        public float Quantity { get; set; }

    }

    public enum OrderStatus
    {
        Pending,
        InProgress,
        Success,
        Failed
    }
}
