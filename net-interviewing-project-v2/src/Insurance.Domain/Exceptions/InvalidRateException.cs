using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Exceptions
{
    public class InvalidRateException : Exception
    {
		public InvalidRateException()
		{
		}

		public InvalidRateException(string message) : base(message)
		{

		}

		public InvalidRateException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
