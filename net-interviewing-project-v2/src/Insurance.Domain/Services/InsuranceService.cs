using Insurance.Domain.Dtos.Insurance;
using Insurance.Domain.Dtos.Product;
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

        public async Task<InsuranceProductDto> GetInsuranceForProductAsync(int productId)
        {
            var product = await _productService.GetProductAsync(productId);

            var insuranceProduct = new InsuranceProductDto();

            insuranceProduct.Product = product;
            insuranceProduct.InsuranceCost = CalculateInusranceCostForProduct(product);

            return insuranceProduct;
        }

        private float CalculateInusranceCostForProduct(ProductDto product)
        {
            float cost = 0f;
            if (product.CanBeInsured)
            {
                if (product.SalesPrice >= 500 && product.SalesPrice < 2000)
                {
                    cost += _insuranceSettingsService.GetInsuranceCostForSalesPriceBetween500And2000();
                }
                else if (product.SalesPrice >= 2000)
                {
                    cost += _insuranceSettingsService.GetInsuranceCostForSalesPriceGreaterThan2000();
                }

                if (_insuranceSettingsService.GetInsurableSpeacialProductTypes().Any(c=>c == product.ProductTypeName))
                {
                    cost += _insuranceSettingsService.GetInsuranceCostForSpecialProducts();
                }
            }
            else
            {
                cost = 0;
            }

            return cost;
        }

        public async Task<float> GetInsuranceForOrderAsync(IList<int> productIds)
        {
            float productInsuranceCost = 0f;

            var productCache = new Dictionary<int, InsuranceProductDto>();

            foreach (var productId in productIds)
            {
                if (productCache.ContainsKey(productId))
                {
                    productInsuranceCost += productCache[productId].InsuranceCost;
                }
                else
                {
                    var insuranceProductDto = await GetInsuranceForProductAsync(productId);
                    productInsuranceCost += insuranceProductDto.InsuranceCost;

                    productCache.Add(productId, insuranceProductDto);
                }
            }

            var specialProductInsuranceCost = CalculateInsuranceCostForSpecialProductsInAnOrder(productCache.Values.Select(c => c.Product).ToList());

            var totalInsuranceCost = productInsuranceCost + specialProductInsuranceCost;

            return totalInsuranceCost;
        }

        private float CalculateInsuranceCostForSpecialProductsInAnOrder(IList<ProductDto> products)
        {
            float cost = 0f;

            var specialProductTypeIds = _insuranceSettingsService.GetInsurableSpeacialProductTypesIdsInAnOrder();

            foreach(var product in products)
            {
                if (specialProductTypeIds.Contains(product.ProductTypeId))
                {
                    cost = _insuranceSettingsService.GetInsuranceCostForSpecialProducts();
                    break;
                }
            }
            return cost;
        }
    }
}
