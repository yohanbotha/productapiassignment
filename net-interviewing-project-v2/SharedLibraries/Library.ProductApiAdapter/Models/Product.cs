using Newtonsoft.Json;

namespace Library.ProductApiAdapter.Models
{
    public class Product
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("salesPrice")]
        public float SalesPrice { get; set; }
        [JsonProperty("productTypeId")]
        public int ProductTypeId { get; set; }
    }
}
