using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using EPAM.WebGallery.Data.Repositories;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Authentication
{
	public class UserIdentity : IIdentity
	{
		public User User { get; set; }

		public string AuthenticationType
		{
			get { return typeof (User).ToString(); }
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

		public void Init(string name, IUserRepository repository)
		{
			if (string.IsNullOrEmpty(name))
			{
				User = repository.GetByLogin(name);
			}
		}
	}
}
