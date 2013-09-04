using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Site.Global.Auth
{
	public interface IAuthentication
	{
		HttpContext HttpContext { get; set; }

		User Login(string login, string password, bool isPersistent);

		User Login(string login);

		void LogOut();

		IPrincipal CurrentUser { get; }
	}
}