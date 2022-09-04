using Insurance.Domain.Data;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Insurance.Domain.Tests.InsuranceServiceTests
{
    public class GetInsuranceForProductAsyncTests
    {
        private readonly Mock<ILogger<InsuranceService>> _logger = new Mock<ILogger<InsuranceService>>();
        private readonly Mock<IProductService> _productService = new Mock<IProductService>();
        private readonly Mock<IInsuranceSettingsService> _insuranceSettingsService = new Mock<IInsuranceSettingsService>();
        private readonly InsuranceDBContext _insuranceDbContext;

        public GetInsuranceForProductAsyncTests()
        {
            _insuranceSettingsService.Setup(c => c.GetInsuranceCostForSalesPriceGreaterThan2000()).Returns(2000);
            _insuranceSettingsService.Setup(c => c.GetInsuranceCostForSalesPriceBetween500And2000()).Returns(1000);
            _insuranceSettingsService.Setup(c => c.GetInsuranceCostForSpecialProducts()).Returns(500);
            _insuranceSettingsService.Setup(c => c.GetInsurableSpeacialProductTypes()).Returns(new List<string> { "Laptops", "Smartphones" });

            var builder = new DbContextOptionsBuilder<InsuranceDBContext>();
            var options = builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _insuranceDbContext = new InsuranceDBContext(options);
        }

        [Fact]
        public async void GetInsuranceValueAsync_NonInsurableProduct_ShouldReturn0()
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

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(0, insuranceProductDto.InsuranceCost);
        }

        [Fact]
        public async void GetInsuranceValueAsync_InsurableProduct_GivenSalesPriceLessThan500Euros_ShouldReturn0()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 499,
                    ProductTypeName = "Test Product Type",
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(0, insuranceProductDto.InsuranceCost);
        }

        [Fact]
        public async void GetInsuranceValueAsync_InsurableProduct_GivenSalesPriceBetween500And2000Euros_ShouldReturn1000()
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
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(1000, insuranceProductDto.InsuranceCost);
        }

        [Fact]
        public async void GetInsuranceValueAsync_InsurableProduct_GivenSalesPriceGreaterThan2000Euros_ShouldReturn2000()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 2000,
                    ProductTypeName = "Test Product Type",
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(2000, insuranceProductDto.InsuranceCost);
        }

        [Fact]
        public async void GetInsuranceValueAsync_InsurableLaptopProduct_GivenSalesPriceLessThan500Euros_ShouldReturn500()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 499,
                    ProductTypeName = "Laptops",
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(500, insuranceProductDto.InsuranceCost);
        }

        [Fact]
        public async void GetInsuranceValueAsync_InsurableLaptopProduct_GivenSalesPriceBetween500And2000Euros_ShouldReturn1500()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 500,
                    ProductTypeName = "Laptops",
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(1500, insuranceProductDto.InsuranceCost);
        }

        [Fact]
        public async void GetInsuranceValueAsync_InsurableLaptopProduct_GivenSalesPriceGreaterThan2000Euros_ShouldReturn2500()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 2000,
                    ProductTypeName = "Laptops",
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(2500, insuranceProductDto.InsuranceCost);
        }

        [Fact]
        public async void GetInsuranceValueAsync_InsurableSmartphonesProduct_GivenSalesPriceLessThan500Euros_ShouldReturn500()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 499,
                    ProductTypeName = "Smartphones",
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(500, insuranceProductDto.InsuranceCost);
        }

        [Fact]
        public async void GetInsuranceValueAsync_InsurableSmartphonesProduct_GivenSalesPriceBetween500And2000Euros_ShouldReturn1500()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 500,
                    ProductTypeName = "Smartphones",
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(1500, insuranceProductDto.InsuranceCost);
        }

        [Fact]
        public async void GetInsuranceValueAsync_InsurableSmartphonesProduct_GivenSalesPriceGreaterThan2000Euros_ShouldReturn2500()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 2000,
                    ProductTypeName = "Smartphones",
                    CanBeInsured = true
                });

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(2500, insuranceProductDto.InsuranceCost);
        }

        [Fact]
        public async void GetInsuranceValueAsync_ProductTypeWithSurcharge_ShouldAddedToTheInsuranceCost()
        {
            // Setup
            _productService.Setup(c => c.GetProductAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    ProductTypeId = 1,
                    SalesPrice = 2000,
                    ProductTypeName = "Smartphones",
                    CanBeInsured = true
                });

            _insuranceDbContext.Rates.Add(
                new Data.Entities.Rate
                {
                    ProductTypeId = 1,
                    SurchargeRate = 10
                });
            _insuranceDbContext.SaveChanges();

            var service = new InsuranceService(_logger.Object, _productService.Object, _insuranceSettingsService.Object, _insuranceDbContext);

            // Act
            var insuranceProductDto = await service.GetInsuranceForProductAsync(10);

            // Assert
            Assert.Equal(2750, insuranceProductDto.InsuranceCost);
        }
    }
}
