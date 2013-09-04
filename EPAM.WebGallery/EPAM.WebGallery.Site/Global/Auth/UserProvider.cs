using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using EPAM.WebGallery.Contracts;
using EPAM.WebGallery.Data.Repositories;

namespace EPAM.WebGallery.Site.Global.Auth
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

		public UserProvider(string name, IMembershipService service)
		{
			userIdentity = new UserIdentity();
			userIdentity.Init(name, service);
		}

		public override string ToString()
		{
			return userIdentity.Name;
		}
	}
}