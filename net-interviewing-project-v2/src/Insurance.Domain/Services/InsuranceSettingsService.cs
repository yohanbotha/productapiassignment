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

        public IList<string> GetInsurableSpeacialProducts()
        {
            return new List<string> { "Laptops", "Smartphones" };
        }

        public float GetInsuranceCostForSpeacialProducts()
        {
            return 500f;
        }

        public float GetMaximumInsuranceCost()
        {
            return 2000f;
        }

        public float GetMinimumInsuranceCost()
        {
            return 1000f;
        }
    }
}
