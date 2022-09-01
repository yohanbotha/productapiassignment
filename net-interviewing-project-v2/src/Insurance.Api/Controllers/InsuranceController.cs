using Insurance.Api.ExceptionFilters;
using Insurance.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

            var insuranceValue = await _insuranceService.GetInsuranceValueAsync(productId);

            return Ok(new { insuranceValue = insuranceValue});
        }
    }
}
