using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EPAM.WebGallery.Authentication
{
	public class AuthHttpModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += Authenticate;
		}

		private void Authenticate(Object source, EventArgs e)
		{
			var app = (HttpApplication) source;
			HttpContext context = app.Context;

			var auth = new CustomAuthentication();
			auth.HttpContext = context;
			context.User = auth.CurrentUser;
		}

		public void Dispose()
		{
		}
	}
}

