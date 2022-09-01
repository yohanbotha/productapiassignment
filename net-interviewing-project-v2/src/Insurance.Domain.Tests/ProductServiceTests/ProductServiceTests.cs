using Insurance.Domain.Dtos.Product;
using Insurance.Domain.Services;
using Library.ProductApiAdapter;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Insurance.Domain.Tests.ProductServiceTests
{
    public class ProductServiceTests
    {
        private readonly Mock<ILogger<ProductService>> _logger = new Mock<ILogger<ProductService>>();
        private readonly Mock<IProductDataApiClient> _productClient = new Mock<IProductDataApiClient>();

        public ProductServiceTests()
        {

        }

        [Fact]
        public async void GetProductAsync_ValidProduct_ShouldReturnValidProduct()
        {
            // Setup
            _productClient.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Library.ProductApiAdapter.Models.Product
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 500
                });

            _productClient.Setup(c => c.GetProductTypeAsync(1)).ReturnsAsync(
                new Library.ProductApiAdapter.Models.ProductType
                {
                    Id = 1,
                    Name = "Test Product Type",
                    CanBeInsured = true
                });

            var service = new ProductService(_logger.Object, _productClient.Object);

            // Act
            var productDto = await service.GetProductAsync(10);

            // Assert
            Assert.Equal(10, productDto.Id);
            Assert.Equal("Test Product", productDto.Name);
            Assert.Equal(1, productDto.ProductTypeId);
            Assert.Equal(500, productDto.SalesPrice);
            Assert.Equal("Test Product Type", productDto.ProductTypeName);
            Assert.True(productDto.CanBeInsured);
        }
    }
}
