using Insurance.Domain.Dtos.Product;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain
{
    public class InsuranceService : IInsuranceService
    {
        private readonly ILogger<InsuranceService> _logger;
        private readonly IProductService _productService;

        public InsuranceService(ILogger<InsuranceService> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<float> GetInsuranceValueAsync(int productId)
        {
            var product = await _productService.GetProductAsync(productId);

            return CalculateInusranceCost(product);
        }

        private float CalculateInusranceCost(ProductDto product)
        {
            float cost = 0f;
            if (product.CanBeInsured)
            {
                if (product.SalesPrice >= 500 && product.SalesPrice < 2000)
                {
                    cost += 1000;
                }
                else if (product.SalesPrice >= 2000)
                {
                    cost += 2000;
                }

                if (product.ProductTypeName == "Laptops" || product.ProductTypeName == "Smartphones")
                {
                    cost += 500;
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
