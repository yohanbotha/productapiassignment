namespace Insurance.Domain.Interfaces
{
    public interface IInsuranceService
    {
        Task<float> GetInsuranceForProductAsync(int productId);

        Task<float> GetInsuranceForOrderAsync(IList<int> productIds);
    }
}
