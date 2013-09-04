using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Contracts.Exceptions
{
	public class DuplicateLoginException : MembershipServiceException
	{
		public string Login { get; set; }
	}
}
