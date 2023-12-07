using Bushido.TestTask.Library.Core.Models;
using Newtonsoft.Json;

namespace Bushido.TestTask.Cloud.CryptoTrading.Models
{
    public class Cryptocurrency: BaseDBModel
    {
        public Cryptocurrency(string name) :base()
        {
            Name = name;
        }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
