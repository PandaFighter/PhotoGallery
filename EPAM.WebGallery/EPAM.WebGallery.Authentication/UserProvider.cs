using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using EPAM.WebGallery.Data.Repositories;

namespace EPAM.WebGallery.Authentication
{
	public class UserProvider : IPrincipal
	{
		private UserIdentity userIdentity { get; set; }

		public IIdentity Identity
		{
			get { return userIdentity; }
		}

		public bool IsInRole(string role)
		{
			if (userIdentity.User == null)
			{
				return false;
			}
			return userIdentity.User.IsInRole(role);
		}

		public UserProvider(string name, IUserRepository repository)
		{
			userIdentity = new UserIdentity();
			userIdentity.Init(name, repository);
		}

		public override string ToString()
		{
			return userIdentity.Name;
		}
	}
}
