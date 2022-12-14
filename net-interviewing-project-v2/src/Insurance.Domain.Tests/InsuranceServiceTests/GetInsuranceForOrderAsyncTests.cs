using Insurance.Domain.Data;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Services;
using Microsoft.EntityFrameworkCore;
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
        private readonly InsuranceDBContext _insuranceDbContext;

        public GetInsuranceForOrderAsyncTests()
        {
            _insuranceSettingsService.Setup(c => c.GetInsuranceCostForSalesPriceGreaterThan2000()).Returns(2000);
            _insuranceSettingsService.Setup(c => c.GetInsuranceCostForSalesPriceBetween500And2000()).Returns(1000);
            _insuranceSettingsService.Setup(c => c.GetInsuranceCostForSpecialProducts()).Returns(500);
            _insuranceSettingsService.Setup(c => c.GetInsurableSpeacialProductTypes()).Returns(new List<string> { "Laptops", "Smartphones" });
            _insuranceSettingsService.Setup(c => c.GetInsurableSpeacialProductTypesIdsInAnOrder()).Returns(new List<int> { 32, 33, 35 });
            _insuranceSettingsService.Setup(c => c.GetInsuranceCostForSpeacialProductsInAnOrder()).Returns(500);

            var builder = new DbContextOptionsBuilder<InsuranceDBContext>();
            var options = builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _insuranceDbContext = new InsuranceDBContext(options);
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

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

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

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceValue = await service.GetInsuranceForOrderAsync(new List<int> { 10, 20, 20, 20 });

            // Assert
            Assert.Equal(3000, insuranceValue);
        }

        [Fact]
        public async void GetInsuranceForOrderAsync_OrderWithSpecialProducts_ShouldReturnCost()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 32,
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

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceValue = await service.GetInsuranceForOrderAsync(new List<int> { 10, 20 });

            // Assert
            Assert.Equal(1500, insuranceValue);
        }
    }
}
