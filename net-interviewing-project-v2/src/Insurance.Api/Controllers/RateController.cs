using Insurance.Api.ExceptionFilters;
using Insurance.Api.Models;
using Insurance.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    [CustomExeption]
    public class RateController : Controller
    {
        private readonly ILogger<RateController> _logger;
        private readonly IRateService _rateService;

        public RateController(ILogger<RateController> logger, IRateService rateService)
        {
            _logger = logger;
            _rateService = rateService;
        }

        [HttpPost]
        [Route("api/rate")]
        public async Task<ActionResult> CreateSurchargeRate([FromBody] RateRequest request)
        {
            _logger.LogInformation($"CreateSurchargeRate api invoked: PropertyTypeId {request.Id}, SurchargeRate: {request.SurchargeRate}");

            var response = await _rateService.CreateRateAsync(
                new Domain.Dtos.Rate.RateDto
                {
                    ProductTypeId = request.Id,
                    SurchargeRate = request.SurchargeRate
                });

            return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
