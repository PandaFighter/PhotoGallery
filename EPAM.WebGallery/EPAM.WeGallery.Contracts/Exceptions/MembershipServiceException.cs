using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Contracts.Exceptions
{
	[Serializable]
	public class MembershipServiceException : Exception
	{
		public MembershipServiceException(Exception exception) : base("Inner exception " + exception.Message, exception)
		{
			
		}

		public MembershipServiceException(string message) : base(message)
		{
			
		}

		public MembershipServiceException()
		{
			
		}

	}
}
