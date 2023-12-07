using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Bushido.TestTask.Library.Core.Models
{
    public class BaseDBModel
    {
        public BaseDBModel() => Id = Guid.NewGuid().ToString();

        [Key]
        [JsonProperty("id")]
        public string? Id { get; set; }
    }
}