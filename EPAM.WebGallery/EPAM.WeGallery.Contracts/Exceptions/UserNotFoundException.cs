using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Contracts.Exceptions
{
	public class UserNotFoundException : MembershipServiceException
	{
		public Guid UserId { get; set; }

		public string UserLogin { get; set; }
		
	}
}
