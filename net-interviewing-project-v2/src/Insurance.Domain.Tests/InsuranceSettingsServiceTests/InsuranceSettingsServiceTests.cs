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
        public void GetInsurableSpeacialProducts_ShouldReturnSpecialProducts()
        {
            var service = new InsuranceSettingsService(_logger.Object);

            // Act
            var insurableProduts = service.GetInsurableSpeacialProducts();

            // Assert
            Assert.Equal(insurableProduts, new List<string> { "Laptops", "Smartphones" });
        }

        [Fact]
        public void GetMinimumInsuranceCost_ShouldReturnMinimumValue()
        {
            var service = new InsuranceSettingsService(_logger.Object);

            // Act
            var cost = service.GetMinimumInsuranceCost();

            // Assert
            Assert.Equal(1000f, cost);
        }

        [Fact]
        public void GetMaximumInsuranceCost_ShouldReturnMaximumValue()
        {
            var service = new InsuranceSettingsService(_logger.Object);

            // Act
            var cost = service.GetMaximumInsuranceCost();

            // Assert
            Assert.Equal(2000f, cost);
        }

        [Fact]
        public void GetInsuranceCostForSpeacialProducts_ShouldReturnInsuranceCostForSpeacialProducts()
        {
            var service = new InsuranceSettingsService(_logger.Object);

            // Act
            var cost = service.GetInsuranceCostForSpeacialProducts();

            // Assert
            Assert.Equal(500f, cost);
        }
    }
}
