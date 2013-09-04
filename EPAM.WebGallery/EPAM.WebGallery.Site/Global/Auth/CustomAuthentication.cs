using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using EPAM.WebGallery.Contracts;
using EPAM.WebGallery.Model;
using EPAM.WebGallery.Site.Helpers;
using Ninject;

namespace EPAM.WebGallery.Site.Global.Auth
{
	public class CustomAuthentication : IAuthentication
	{
		private const string cookieName = "__AUTH_COOKIE";

		public IPrincipal _currentuser;

		public CustomAuthentication()
		{
		}

		[Inject]
		public IMembershipService MembershipService { get; set; }

		public HttpContext HttpContext { get; set; }

		public User Login(string userName)
		{
			User user = MembershipService.GetUserByLogin(userName);
			if (user != null)
			{
				CreateCookie(userName);
			}
			return user;
		}

		public User Login(string login, string password, bool isPersistent)
		{
			User user = MembershipService.GetUserByLogin(login);
			if (user.VerifyPassword(password))
			{
				CreateCookie(login, isPersistent);
			}
			return user;
		}

		public void LogOut()
		{
			HttpCookie httpCookie = HttpContext.Response.Cookies[cookieName];
			if (httpCookie != null)
			{
				httpCookie.Value = string.Empty;
			}
		}

		public IPrincipal CurrentUser
		{
			get
			{
				if (_currentuser == null)
				{
					try
					{
						HttpCookie cookie = HttpContext.Request.Cookies[cookieName];
						if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
						{
							FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
							_currentuser = new UserProvider(ticket.Name, MembershipService);
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

		private void CreateCookie(string login, bool isPersistent = false)
		{
			var ticket = new FormsAuthenticationTicket(
				1,
				login,
				DateTime.Now,
				DateTime.Now.Add(FormsAuthentication.Timeout),
				isPersistent,
				string.Empty,
				FormsAuthentication.FormsCookiePath
				);
			string encTicket = FormsAuthentication.Encrypt(ticket);

			var authCookie = new HttpCookie(cookieName) {
				Value = encTicket,
				Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
			};
			HttpContext.Response.Cookies.Set(authCookie);
		}
	}
}