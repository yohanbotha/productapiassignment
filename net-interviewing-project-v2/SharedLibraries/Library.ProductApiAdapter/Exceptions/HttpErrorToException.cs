using System.Net;

namespace Library.ProductApiAdapter.Exceptions
{
    internal static class HttpErrorToException
    {
        public static Exception Create(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new ProductDataNotFoundException("Product data not found");
            }
            else
            {
                return new Exception("Exception occurred while connecting to ProductDataApi");
            }
        }
    }
}
