using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using EPAM.WebGallery.Model;
using EPAM.WebGallery.Site.Global.Auth;
using Ninject;

namespace EPAM.WebGallery.Site.Controllers
{
	public class BaseController : Controller
	{
		[Inject]
		public IAuthentication Auth { get; set; }

		public static string HostName = string.Empty;

		protected static string ErrorPage = "~/Error";

		protected static string NotFoundPage = "~/NotFoundPage";

		protected static string LoginPage = "~/Login/Index";

		public string CurrentLangCode { get; protected set; }
	

		public User CurrentUser
		{
			get { return ((IUserProvider) Auth.CurrentUser.Identity).User; }
		}

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			if (requestContext.HttpContext.Request.Url != null)
			{
				HostName = requestContext.HttpContext.Request.Url.Authority;
			}
			base.Initialize(requestContext);
			if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
			{
				CurrentLangCode = requestContext.RouteData.Values["lang"] as string;
				if (string.IsNullOrEmpty(CurrentLangCode))
					CurrentLangCode = "ru";
				var ci = new CultureInfo(CurrentLangCode);
				Thread.CurrentThread.CurrentUICulture = ci;
				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
			}
		}
	}
}