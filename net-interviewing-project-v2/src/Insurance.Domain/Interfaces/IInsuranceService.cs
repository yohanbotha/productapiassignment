namespace Insurance.Domain
{
    public interface IInsuranceService
    {
        Task<float> GetInsuranceValueAsync(int productId);
    }
}
