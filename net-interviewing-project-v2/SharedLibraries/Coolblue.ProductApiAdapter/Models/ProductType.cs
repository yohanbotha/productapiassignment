using Newtonsoft.Json;

namespace Coolblue.ProductApiAdapter.Models
{
    public class ProductType
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("canBeInsured")]
        public bool CanBeInsured { get; set; }
    }
}
