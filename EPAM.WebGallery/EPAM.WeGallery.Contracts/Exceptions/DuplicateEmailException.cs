using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Contracts.Exceptions
{
	public class DuplicateEmailException : MembershipServiceException
	{
		public string Email { get; set; }

	}
}
