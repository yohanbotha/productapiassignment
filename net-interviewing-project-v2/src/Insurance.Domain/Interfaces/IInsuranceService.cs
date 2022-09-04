using Insurance.Domain.Dtos.Insurance;

namespace Insurance.Domain.Interfaces
{
    public interface IInsuranceService
    {
        Task<InsuranceProductDto> GetInsuranceForProductAsync(int productId);

        Task<float> GetInsuranceForOrderAsync(IList<int> productIds);
    }
}
