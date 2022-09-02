using Insurance.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Insurance.Domain.Tests.InsuranceSettingsServiceTests
{
    public class InsuranceSettingsServiceTests
    {
        private readonly Mock<ILogger<InsuranceSettingsService>> _logger = new Mock<ILogger<InsuranceSettingsService>>();

        public InsuranceSettingsServiceTests()
        {

        }

        [Fact]
        public void GetInsurableSpeacialProductTypes_ShouldReturnSpecialProductTypes()
        {
            var service = new InsuranceSettingsService(_logger.Object);

            // Act
            var insurableProduts = service.GetInsurableSpeacialProductTypes();

            // Assert
            Assert.Equal(insurableProduts, new List<string> { "Laptops", "Smartphones" });
        }

        [Fact]
        public void GetInsuranceCostForSalesPriceBetween500And2000_ShouldInsuranceCost()
        {
            var service = new InsuranceSettingsService(_logger.Object);

            // Act
            var cost = service.GetInsuranceCostForSalesPriceBetween500And2000();

            // Assert
            Assert.Equal(1000f, cost);
        }

        [Fact]
        public void GetInsuranceCostForSalesPriceGreaterThan2000_ShouldInsuranceCost()
        {
            var service = new InsuranceSettingsService(_logger.Object);

            // Act
            var cost = service.GetInsuranceCostForSalesPriceGreaterThan2000();

            // Assert
            Assert.Equal(2000f, cost);
        }

        [Fact]
        public void GetInsuranceCostForSpeacialProducts_ShouldReturnInsuranceCostForSpeacialProducts()
        {
            var service = new InsuranceSettingsService(_logger.Object);

            // Act
            var cost = service.GetInsuranceCostForSpecialProducts();

            // Assert
            Assert.Equal(500f, cost);
        }

        [Fact]
        public void GetInsurableSpeacialOrderProductTypeIds_ShouldReturnSpecialProductTypeIdss()
        {
            var service = new InsuranceSettingsService(_logger.Object);

            // Act
            var insurableProduts = service.GetInsurableSpeacialOrderProductTypeIds();

            // Assert
            Assert.Equal(insurableProduts, new List<int> { 32, 33, 35 });
        }
    }
}
