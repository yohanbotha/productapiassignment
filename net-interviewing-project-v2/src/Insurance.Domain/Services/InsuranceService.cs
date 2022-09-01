﻿using Insurance.Domain.Dtos.Product;
using Insurance.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Insurance.Domain.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly ILogger<InsuranceService> _logger;
        private readonly IProductService _productService;
        private readonly IInsuranceSettingsService _insuranceSettingsService;

        public InsuranceService(ILogger<InsuranceService> logger, IProductService productService, IInsuranceSettingsService insuranceSettingsService)
        {
            _logger = logger;
            _productService = productService;
            _insuranceSettingsService = insuranceSettingsService;
        }

        public async Task<float> GetInsuranceValueAsync(int productId)
        {
            var product = await _productService.GetProductAsync(productId);

            return CalculateInusranceCostForProduct(product);
        }

        private float CalculateInusranceCostForProduct(ProductDto product)
        {
            float cost = 0f;
            if (product.CanBeInsured)
            {
                if (product.SalesPrice >= 500 && product.SalesPrice < 2000)
                {
                    cost += _insuranceSettingsService.GetMinimumInsuranceCost();
                }
                else if (product.SalesPrice >= 2000)
                {
                    cost += _insuranceSettingsService.GetMaximumInsuranceCost();
                }

                if (_insuranceSettingsService.GetInsurableSpeacialProducts().Any(c=>c == product.ProductTypeName))
                {
                    cost += _insuranceSettingsService.GetInsuranceCostForSpeacialProducts();
                }
            }
            else
            {
                cost = 0;
            }

            return cost;
        }
    }
}
