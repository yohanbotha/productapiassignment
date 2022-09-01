using Library.ProductApiAdapter;
using Insurance.Domain.Dtos.Product;
using Microsoft.Extensions.Logging;
using Insurance.Domain.Interfaces;

namespace Insurance.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductDataApiClient _productDataApiClient;

        public ProductService(ILogger<ProductService> logger, IProductDataApiClient productDataApiClient)
        {
            _logger = logger;
            _productDataApiClient = productDataApiClient;
        }

        public async Task<ProductDto> GetProductAsync(int productId)
        {
            var product = await _productDataApiClient.GetProductAsync(productId);

            var productType = await _productDataApiClient.GetProductTypeAsync(product.ProductTypeId);

            return new ProductDto 
            { 
                Id = product.Id, 
                Name = product.Name, 
                SalesPrice = product.SalesPrice, 
                ProductTypeId = productType.Id, 
                ProductTypeName = productType.Name, 
                CanBeInsured = productType.CanBeInsured 
            };
        }
    }
}
