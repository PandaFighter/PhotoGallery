using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPAM.WebGallery.Model;
using EPAM.WebGallery.Services;

namespace EPAM.WebGallery.Site.Helpers
{
	public class AuthorizeUserAttribute : AuthorizeAttribute
	{
		public string AccessLevel { get; set; }

		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			var isAuthorized = base.AuthorizeCore(httpContext);
			if (!isAuthorized)
			{
				return false;
			}
			var user = (new MembershipService()).GetUserByLogin(httpContext.User.Identity.Name);

			if (user.IsInRole(AccessLevel))
			{
				return true;
			}
			else
			{
				return false;
			}
			return base.AuthorizeCore(httpContext);
		}
	}
}