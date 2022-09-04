using Insurance.Api.ExceptionFilters;
using Insurance.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    [CustomExeption]
    public class InsuranceController : Controller
    {
        private readonly ILogger<InsuranceController> _logger;
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(ILogger<InsuranceController> logger, IInsuranceService insuranceService)
        {
            _logger = logger;
            _insuranceService = insuranceService;
        }

        [HttpGet]
        [Route("api/insurance/products/{productId}")]
        public async Task<ActionResult> CalculateInsuranceForProduct([FromRoute] int productId)
        {
            _logger.LogInformation($"CalculateInsuranceForProduct api invoked: {productId}");

            var insuranceProductDto = await _insuranceService.GetInsuranceForProductAsync(productId);

            return Ok(new { insuranceValue = insuranceProductDto.InsuranceCost });
        }

        [HttpPost]
        [Route("api/insurance/order")]
        public async Task<ActionResult> CalculateInsuranceForOrder([FromBody] List<int> productIds)
        {
            _logger.LogInformation($"CalculateInsuranceForOrder api invoked: {string.Join(", ", productIds)}");

            var insuranceValue = await _insuranceService.GetInsuranceForOrderAsync(productIds);

            return Ok(new { insuranceValue = insuranceValue });
        }
    }
}
