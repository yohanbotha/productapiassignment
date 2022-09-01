using Library.ProductApiAdapter.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Insurance.Api.ExceptionFilters
{
    public class CustomExeptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            if (typeof(ProductDataNotFoundException) == ex.GetType())
            {
                Log.ForContext<CustomExeptionAttribute>().Error($"Resource not found {context.HttpContext.Request.Path}", ex);

                context.Result = new NotFoundObjectResult(
                    new
                    {
                        Error = ex.Message
                    });
            }
            else
            {
                Log.ForContext<CustomExeptionAttribute>().Error($"Invalid request body {context.HttpContext.Request.Path}", ex);

                context.Result = new BadRequestObjectResult(
                    new
                    {
                        Error = ex.Message
                    });
            }
        }
    }
}
