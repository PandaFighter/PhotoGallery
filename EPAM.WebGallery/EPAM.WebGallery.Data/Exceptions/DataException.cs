using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Data.Exceptions
{
	public class DataException : Exception
	{
		public DataException(string message, Exception exception) : base(message, exception)
		{
		}

		public DataException(string message) : base(message)
		{
		}
	}
}
