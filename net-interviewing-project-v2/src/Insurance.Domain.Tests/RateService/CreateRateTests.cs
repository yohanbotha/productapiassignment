using Insurance.Domain.Data;
using Insurance.Domain.Exceptions;
using Insurance.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Insurance.Domain.Tests.RateService
{
    public class CreateRateTests
    {
        private readonly Mock<ILogger<Services.RateService>> _logger = new Mock<ILogger<Services.RateService>>();
        private readonly Mock<IProductService> _productService = new Mock<IProductService>();
        private readonly InsuranceDBContext _insuranceDbContext;

        public CreateRateTests()
        {
            var builder = new DbContextOptionsBuilder<InsuranceDBContext>();
            var options = builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _insuranceDbContext = new InsuranceDBContext(options);
        }

        [Fact]
        public void CreateRate_InvalidSurchargeValue_ShouldThrowProductDataNotFoundException()
        {
            var service = new Services.RateService(_logger.Object, _productService.Object, _insuranceDbContext);

            // Assert
            Assert.ThrowsAsync<InvalidRateException>(() => service.CreateRateAsync(new Dtos.Rate.RateDto { ProductTypeId = 10, SurchargeRate = 0 }));
            Assert.ThrowsAsync<InvalidRateException>(() => service.CreateRateAsync(new Dtos.Rate.RateDto { ProductTypeId = 10, SurchargeRate = 101 }));
        }

        [Fact]
        public async Task CreateRate_ShouldAddNewRecord()
        {
            // Setup
            _productService.Setup(c => c.GetProductTypeAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductTypeDto
                {
                    Id = 10,
                    Name = "Test Product Type",
                    CanBeInsured = false
                });

            var service = new Services.RateService(_logger.Object, _productService.Object, _insuranceDbContext);

            // Act

            var rateDto = await service.CreateRateAsync(new Dtos.Rate.RateDto { ProductTypeId = 10, SurchargeRate = 5 });

            // Assert

            var dbRate = await _insuranceDbContext.Rates.FirstOrDefaultAsync(c => c.ProductTypeId == rateDto.ProductTypeId);

            Assert.Equal(10, dbRate.ProductTypeId);
            Assert.Equal(5, dbRate.SurchargeRate);
        }

        [Fact]
        public async Task CreateRate_ShouldUpdateRecord()
        {
            // Setup
            _productService.Setup(c => c.GetProductTypeAsync(10)).ReturnsAsync(
                new Dtos.Product.ProductTypeDto
                {
                    Id = 1,
                    Name = "Test Product Type",
                    CanBeInsured = false
                });

            _insuranceDbContext.Rates.Add(
                new Data.Entities.Rate
                {
                    ProductTypeId = 1,
                    SurchargeRate = 10
                });
            _insuranceDbContext.SaveChanges();

            var service = new Services.RateService(_logger.Object, _productService.Object, _insuranceDbContext);

            // Act

            var rateDto = await service.CreateRateAsync(new Dtos.Rate.RateDto { ProductTypeId = 1, SurchargeRate = 5 });

            // Assert

            var dbRate = await _insuranceDbContext.Rates.FirstOrDefaultAsync(c => c.ProductTypeId == rateDto.ProductTypeId);
            Assert.Equal(1, dbRate.ProductTypeId);
            Assert.Equal(5, dbRate.SurchargeRate);
        }
    }
}
