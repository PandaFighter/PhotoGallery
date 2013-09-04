using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EPAM.WebGallery.Site.App_Start
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "new",
				url: "{lang}/About",
				defaults: new {controller = "Home", action = "About", id = UrlParameter.Optional}
				);

			//routes.MapRoute(
			//	name: "album",
			//	url: "{lang}/Gallery/{action}/{name}", 
			//	defaults:new {controller = "Gallery", action = "Album", id = UrlParameter.Optional}
			//	);

            routes.MapRoute(
                name: "lang",
                url: "{lang}/{controller}/{action}/{name}",
                defaults: new { controller = "Home", action = "Index", name = UrlParameter.Optional },
                constraints : new { lang = @"ru|en" },
                namespaces: new[] { "EPAM.WebGallery.Site.Controllers" }
            );

            routes.MapRoute(
                name : "default",
                url : "{controller}/{action}/{id}",
                defaults : new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "en" },
                namespaces : new [] { "EPAM.WebGallery.Site.Controllers" }
            );
		}
	}
}