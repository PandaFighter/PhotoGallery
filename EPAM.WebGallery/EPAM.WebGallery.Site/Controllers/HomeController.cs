using System.Web.Mvc;

namespace EPAM.WebGallery.Site.Controllers
{
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}