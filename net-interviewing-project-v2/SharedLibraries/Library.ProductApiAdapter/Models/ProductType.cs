using Newtonsoft.Json;

namespace Library.ProductApiAdapter.Models
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
