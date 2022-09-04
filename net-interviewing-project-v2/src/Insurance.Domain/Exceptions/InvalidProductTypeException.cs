using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Exceptions
{
    public class InvalidProductTypeException : Exception
    {
		public InvalidProductTypeException()
		{
		}

		public InvalidProductTypeException(string message) : base(message)
		{

		}

		public InvalidProductTypeException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
