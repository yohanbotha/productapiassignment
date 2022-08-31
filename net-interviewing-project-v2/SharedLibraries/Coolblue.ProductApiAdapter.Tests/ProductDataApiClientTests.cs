using Coolblue.ProductApiAdapter.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace Coolblue.ProductApiAdapter.Tests
{
    public class ProductDataApiClientTests
    {
        private readonly IProductDataApiClient _client;
        private const string _productApi = "http://localhost:5002";

        public ProductDataApiClientTests()
        {
            _client = new ProductDataApiClient(new HttpClient(), Options.Create(new Configuration.ProductDataApiConfiguration
            {
                BaseUrl = _productApi
            }));
        }

        [Fact]
        public async Task ProductDataApiClient_GetProducts_ShouldReturnData()
        {
            var products = await _client.GetProductsAsync();

            Assert.True(products.Count(c => c.Id > 0) > 0);
        }

        [Fact]
        public async Task ProductDataApiClient_GetProductById_ShouldReturnProduct()
        {
            var product = await _client.GetProductByIdAsync(725435);

            Assert.Equal(725435, product.Id);
        }

        [Fact]
        public async Task ProductDataApiClient_GetProductTypes_ShouldReturnData()
        {
            var productTypes = await _client.GetProductTypesAsync();

            Assert.True(productTypes.Count(c=>c.Name == "Laptops") > 0);
        }

        [Fact]
        public async Task ProductDataApiClient_GetProductTypeById_ShouldReturnProductType()
        {
            var productType = await _client.GetProductTypeByIdAsync(124);

            Assert.Equal(124, productType.Id);
        }

        [Fact]
        public void ProductDataApiClient_GetInvalidProduct_ShouldThrowNotFoundException()
        {
            Assert.ThrowsAsync<ProductDataNotFoundException>(() => _client.GetProductTypeByIdAsync(123456));
        }

        [Fact]
        public void ProductDataApiClient_InvalidEndpoint_ShouldThrowException()
        {
            IProductDataApiClient invalidClient = new ProductDataApiClient(new HttpClient(), Options.Create(new Configuration.ProductDataApiConfiguration
            {
                BaseUrl = $"https://www.coolblue.nl/en"
            }));
            Assert.ThrowsAsync<Exception>(() => _client.GetProductByIdAsync(725435));
        }
    }
}