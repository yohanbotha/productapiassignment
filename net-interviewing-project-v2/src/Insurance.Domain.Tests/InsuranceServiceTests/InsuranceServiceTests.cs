using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Insurance.Domain.Tests.InsuranceServiceTests
{
    public class InsuranceServiceTests
    {
        private readonly Mock<ILogger<InsuranceService>> _logger = new Mock<ILogger<InsuranceService>>();
        private readonly Mock<IProductService> _productService = new Mock<IProductService>();

        public InsuranceServiceTests()
        {

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

            var service = new InsuranceService(_logger.Object, _productService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceValueAsync(10);

            // Assert
            Assert.Equal(0, insuranceValue);
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

            var service = new InsuranceService(_logger.Object, _productService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceValueAsync(10);

            // Assert
            Assert.Equal(0, insuranceValue);
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

            var service = new InsuranceService(_logger.Object, _productService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceValueAsync(10);

            // Assert
            Assert.Equal(1000, insuranceValue);
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

            var service = new InsuranceService(_logger.Object, _productService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceValueAsync(10);

            // Assert
            Assert.Equal(2000, insuranceValue);
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

            var service = new InsuranceService(_logger.Object, _productService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceValueAsync(10);

            // Assert
            Assert.Equal(500, insuranceValue);
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

            var service = new InsuranceService(_logger.Object, _productService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceValueAsync(10);

            // Assert
            Assert.Equal(1500, insuranceValue);
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

            var service = new InsuranceService(_logger.Object, _productService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceValueAsync(10);

            // Assert
            Assert.Equal(2500, insuranceValue);
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

            var service = new InsuranceService(_logger.Object, _productService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceValueAsync(10);

            // Assert
            Assert.Equal(500, insuranceValue);
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

            var service = new InsuranceService(_logger.Object, _productService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceValueAsync(10);

            // Assert
            Assert.Equal(1500, insuranceValue);
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

            var service = new InsuranceService(_logger.Object, _productService.Object);

            // Act
            var insuranceValue = await service.GetInsuranceValueAsync(10);

            // Assert
            Assert.Equal(2500, insuranceValue);
        }
    }
}
