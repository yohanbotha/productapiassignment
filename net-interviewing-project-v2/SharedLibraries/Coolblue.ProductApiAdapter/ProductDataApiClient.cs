using Coolblue.ProductApiAdapter.Configuration;
using Coolblue.ProductApiAdapter.Exceptions;
using Coolblue.ProductApiAdapter.Models;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<ProductDataApiConfiguration> _configuration;

        public ProductDataApiClient(HttpClient httpClient, IOptions<ProductDataApiConfiguration> configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri($"{_configuration.Value.BaseUrl}");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IList<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("products");

            if (!response.IsSuccessStatusCode)
            {
                throw HttpErrorToException.Create(response);
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            var products = await JsonSerializer.DeserializeAsync<List<Product>>(responseStream);

            return await Task.FromResult(products.ToList());
        }

        public async Task<Product> GetProductsByIdAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"products/{productId}");

            if (!response.IsSuccessStatusCode)
            {
                throw HttpErrorToException.Create(response);
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            var product = await JsonSerializer.DeserializeAsync<Product>(responseStream);

            return await Task.FromResult(product);
        }

        public async Task<IList<ProductType>> GetProductTypesAsync()
        {
            var response = await _httpClient.GetAsync("product_types");

            if (!response.IsSuccessStatusCode)
            {
                throw HttpErrorToException.Create(response);
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(responseStream);

            return await Task.FromResult(productTypes.ToList());
        }

        public async Task<ProductType> GetProductTypeByIdAsync(int productTypeId)
        {
            var response = await _httpClient.GetAsync($"product_types/{productTypeId}");

            if (!response.IsSuccessStatusCode)
            {
                throw HttpErrorToException.Create(response);
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            var productType = await JsonSerializer.DeserializeAsync<ProductType>(responseStream);

            return await Task.FromResult(productType);
        }
    }
}
