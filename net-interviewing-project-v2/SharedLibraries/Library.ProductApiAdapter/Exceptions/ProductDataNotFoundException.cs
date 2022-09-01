namespace Library.ProductApiAdapter.Exceptions
{
    public class ProductDataNotFoundException : Exception
	{
		public ProductDataNotFoundException()
		{
		}

		public ProductDataNotFoundException(string message) : base(message)
		{

		}

		public ProductDataNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
