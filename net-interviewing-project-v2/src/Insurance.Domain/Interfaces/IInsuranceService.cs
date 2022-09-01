namespace Insurance.Domain.Interfaces
{
    public interface IInsuranceService
    {
        Task<float> GetInsuranceValueAsync(int productId);
    }
}
