using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using EPAM.WebGallery.Data.Repositories;
using EPAM.WebGallery.Model;
using EPAM.WebGallery.Services;

namespace EPAM.WebGallery.Authentication
{
	public class CustomAuthentication : IAuthentication
	{
		private const string cookieName = "__AUTH_COOKIE";

		public HttpContext HttpContext { get; set; }

		public IUserRepository Repository { get; set; }

		public User Login(string userName)
		{
			User user = (new MembershipService()).GetUserByLogin(userName);
			if (user != null)
			{
				CreateCookie(userName);
			}
			return user;
		}

		public User Login(string login, string password, bool isPersistent)
		{
			var service = new MembershipService();
			var user = service.GetUserByLogin(login);
			if (user.VerifyPassword(password))
			{
				CreateCookie(login, isPersistent);
			}
			return user;
		}

		private void CreateCookie(string login, bool isPersistent = false)
		{
			var ticket = new FormsAuthenticationTicket(
				1,
				cookieName,
				DateTime.Now,
				DateTime.Now.Add(FormsAuthentication.Timeout),
				isPersistent,
				string.Empty,
				FormsAuthentication.FormsCookiePath
			);
			var encTicket = FormsAuthentication.Encrypt(ticket);

			var authCookie = new HttpCookie(cookieName)
				{
					Value = encTicket,
					Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
				};
			HttpContext.Response.Cookies.Set(authCookie);
		}

		public void LogOut()
		{
			var httpCookie = HttpContext.Response.Cookies[cookieName];
			if (httpCookie != null)
			{
				httpCookie.Value = string.Empty;
			}
		}

		public IPrincipal _currentuser;

		public IPrincipal CurrentUser
		{
			get
			{
				if (_currentuser == null)
				{
					try
					{
						var cookie = HttpContext.Request.Cookies[cookieName];
						if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
						{
							var ticket = FormsAuthentication.Decrypt(cookie.Value);
							_currentuser = new UserProvider(cookie.Value, Repository);
						}
					}
					catch (Exception)
					{
						_currentuser = new UserProvider(null, null);
					}
				}
				return _currentuser;
			}
		}
	}
}
