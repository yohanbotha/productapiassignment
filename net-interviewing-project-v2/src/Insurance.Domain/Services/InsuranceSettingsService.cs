using Insurance.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Insurance.Domain.Services
{
    public class InsuranceSettingsService : IInsuranceSettingsService
    {
        private readonly ILogger<InsuranceSettingsService> _logger;

        public InsuranceSettingsService(ILogger<InsuranceSettingsService> logger)
        {
            _logger = logger;
        }

        public IList<string> GetInsurableSpeacialProductTypes()
        {
            return new List<string> { "Laptops", "Smartphones" };
        }

        public float GetInsuranceCostForSpecialProducts()
        {
            return 500f;
        }

        public float GetInsuranceCostForSalesPriceGreaterThan2000()
        {
            return 2000f;
        }

        public float GetInsuranceCostForSalesPriceBetween500And2000()
        {
            return 1000f;
        }

        public IList<int> GetInsurableSpeacialProductTypesIdsInAnOrder()
        {
            return new List<int> { 32, 33, 35 };
        }

        public float GetInsuranceCostForSpeacialProductsInAnOrder()
        {
            return 500f;
        }
    }
}
