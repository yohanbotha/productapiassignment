namespace Insurance.Domain.Interfaces
{
    public interface IInsuranceSettingsService
    {
        IList<string> GetInsurableSpeacialProductTypes();
        float GetInsuranceCostForSpecialProducts();
        float GetInsuranceCostForSalesPriceBetween500And2000();
        float GetInsuranceCostForSalesPriceGreaterThan2000();
        IList<int> GetInsurableSpeacialOrderProductTypeIds();
    }
}
