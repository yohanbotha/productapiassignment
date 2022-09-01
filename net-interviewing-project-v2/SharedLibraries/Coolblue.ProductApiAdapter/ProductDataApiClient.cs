using Coolblue.ProductApiAdapter.Configuration;
using Coolblue.ProductApiAdapter.Exceptions;
using Coolblue.ProductApiAdapter.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coolblue.ProductApiAdapter
{
    public class ProductDataApiClient : IProductDataApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ProductDataApiConfiguration _configuration;

        public ProductDataApiClient(IOptions<ProductDataApiConfiguration> configuration)
        {
            _configuration = configuration.Value;
            _httpClient = new HttpClient { BaseAddress = new Uri(_configuration.BaseUrl) };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IList<Product>> GetProductsAsync()
        {
            var data = await GetJsonString("/products");

            var products = JsonConvert.DeserializeObject<List<Product>>(data);

            return await Task.FromResult(products.ToList());
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            var data = await GetJsonString($"/products/{productId}");

            var product = JsonConvert.DeserializeObject<Product>(data);

            return await Task.FromResult(product);
        }

        public async Task<IList<ProductType>> GetProductTypesAsync()
        {
            var data = await GetJsonString("/product_types");

            var productTypes = JsonConvert.DeserializeObject<List<ProductType>>(data);

            return await Task.FromResult(productTypes.ToList());
        }

        public async Task<ProductType> GetProductTypeAsync(int productTypeId)
        {
            var data = await GetJsonString($"/product_types/{productTypeId}");

            var productType = JsonConvert.DeserializeObject<ProductType>(data);

            return await Task.FromResult(productType);
        }

        private async Task<string> GetJsonString(string uri)
        {
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw HttpErrorToException.Create(response);
            }

            return await response.Content.ReadAsStringAsync(); ;
        }
    }
}
