using Insurance.Domain.Interfaces;
using Insurance.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Insurance.Domain.Tests.InsuranceServiceTests
{
    public class GetInsuranceForOrderAsyncTests
    {
        private readonly Mock<ILogger<InsuranceService>> _logger = new Mock<ILogger<InsuranceService>>();
        private readonly Mock<IProductService> _productService = new Mock<IProductService>();
        private readonly Mock<IInsuranceSettingsService> _insuranceSettingsService = new Mock<IInsuranceSettingsService>();

        public GetInsuranceForOrderAsyncTests()
        {
            _insuranceSettingsService.Setup(c => c.GetMaximumInsuranceCost()).Returns(2000);
            _insuranceSettingsService.Setup(c => c.GetMinimumInsuranceCost()).Returns(1000);
            _insuranceSettingsService.Setup(c => c.GetInsuranceCostForSpeacialProducts()).Returns(500);
            _insuranceSettingsService.Setup(c => c.GetInsurableSpeacialProducts()).Returns(new List<string> { "Laptops", "Smartphones" });
        }

        [Fact]
        public async void GetInsuranceForOrderAsync_UniqueProductsInOrder_ShouldReturnCost()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 500,
                    ProductTypeName = "Test Product Type",
                    CanBeInsured = false
                });

            _productService.Setup(c => c.GetProductAsync(20)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 20,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 500,
                    ProductTypeName = "Test Product Type",
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceForOrderAsync(new List<int> { 10, 20 });

            // Assert
            Assert.Equal(1000, insuranceValue);
        }

        [Fact]
        public async void GetInsuranceForOrderAsync_DuplicateProductsInOrder_ShouldReturnCost()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 500,
                    ProductTypeName = "Test Product Type",
                    CanBeInsured = false
                });

            _productService.Setup(c => c.GetProductAsync(20)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 20,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 500,
                    ProductTypeName = "Test Product Type",
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceForOrderAsync(new List<int> { 10, 20, 20, 20 });

            // Assert
            Assert.Equal(3000, insuranceValue);
        }
    }
}
