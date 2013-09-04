using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Model
{
	public class Profile : Entity<Guid>
	{
		private User _user;

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int Age { get; set; }

		public string Email { get; set; }
		// enum Country
		public string Country { get; set; }
		//enum City
		public string City { get; set; }

		public string PhoneNumber { get; set; }

		//public Photo Avatar { get; set; }

		public Profile()
		{
			Id = Guid.NewGuid();
		}

	}
}
