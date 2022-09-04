using Insurance.Domain.Dtos.Product;
using Insurance.Domain.Dtos.Rate;

namespace Insurance.Domain.Interfaces
{
    public interface IRateService
    {
        Task<RateDto> CreateRateAsync(RateDto rate);
    }
}
