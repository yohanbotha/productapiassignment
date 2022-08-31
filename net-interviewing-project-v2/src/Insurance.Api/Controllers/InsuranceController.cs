using Insurance.Api.Models.Insurance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    public class InsuranceController : Controller
    {
        private readonly ILogger<InsuranceController> _logger;

        public InsuranceController(ILogger<InsuranceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("api/insurance/products/{productId}")]
        public async Task<ProductInsuranceResponse> CalculateInsuranceForProduct([FromRoute] int productId)
        {
            return new ProductInsuranceResponse()
            {

            };
        }
    }
}
