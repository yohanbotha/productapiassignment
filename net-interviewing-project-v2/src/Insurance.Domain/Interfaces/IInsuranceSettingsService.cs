namespace Insurance.Domain
{
    public interface IInsuranceSettingsService
    {
        IList<string> GetInsurableSpeacialProducts();
        float GetInsuranceCostForSpeacialProducts();
        float GetMinimumInsuranceCost();
        float GetMaximumInsuranceCost();
    }
}
