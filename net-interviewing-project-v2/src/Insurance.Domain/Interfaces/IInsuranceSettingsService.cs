namespace Insurance.Domain.Interfaces
{
    public interface IInsuranceSettingsService
    {
        IList<string> GetInsurableSpeacialProducts();
        float GetInsuranceCostForSpeacialProducts();
        float GetMinimumInsuranceCost();
        float GetMaximumInsuranceCost();
    }
}
