using Library.ProductApiAdapter;
using Insurance.Domain.Dtos.Product;
using Microsoft.Extensions.Logging;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Dtos.Rate;
using Insurance.Domain.Exceptions;
using Insurance.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Domain.Services
{
    public class RateService : IRateService
    {
        private readonly ILogger<RateService> _logger;
        private readonly IProductService _productService;
        private readonly InsuranceDBContext _insuranceDbContext;

        public RateService(ILogger<RateService> logger, IProductService productService, InsuranceDBContext insuranceDBContext)
        {
            _logger = logger;
            _productService = productService;
            _insuranceDbContext = insuranceDBContext;
        }

        public async Task<RateDto> CreateRateAsync(RateDto rate)
        {
            if(rate.SurchargeRate <= 0 || rate.SurchargeRate > 100)
            {
                _logger.LogError($"Invalid surcharge value: {rate.SurchargeRate}");
                throw new InvalidRateException("Invalid surcharge value: Value must be positive percentage");
            }

            var productType = await _productService.GetProductTypeAsync(rate.ProductTypeId);

            var dbRate = await _insuranceDbContext.Rates.FirstOrDefaultAsync(c => c.ProductTypeId == rate.ProductTypeId);

            if(dbRate == null)
            {
                _insuranceDbContext.Rates.Add(
                    new Data.Entities.Rate
                    {
                        ProductTypeId = rate.ProductTypeId,
                        SurchargeRate = rate.SurchargeRate
                    });
            }
            else
            {
                dbRate.ProductTypeId = rate.ProductTypeId;
                dbRate.SurchargeRate = rate.SurchargeRate;
            }

            _insuranceDbContext.SaveChanges();

            return rate;
        }
    }
}
