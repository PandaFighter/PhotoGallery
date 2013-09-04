using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using EPAM.WebGallery.Contracts;
using EPAM.WebGallery.Data.Repositories;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Site.Global.Auth
{
	public class UserIdentity : IIdentity, IUserProvider
	{
		public User User { get; set; }

		public string AuthenticationType
		{
			get { return typeof(User).ToString(); }
		}

		public bool IsAuthenticated
		{
			get { return User != null; }
		}

		public string Name
		{
			get
			{
				if (User != null)
				{
					return User.Login;
				}
				return "anonym";
			}
		}

		public void Init(string name, IMembershipService service)
		{
			if (!string.IsNullOrEmpty(name))
			{
				User = service.GetUserByLogin(name);
			}
		}
	}
}