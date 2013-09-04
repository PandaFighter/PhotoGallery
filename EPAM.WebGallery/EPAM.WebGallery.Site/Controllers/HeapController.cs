using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPAM.WebGallery.Common;
using EPAM.WebGallery.Data.EF;
using EPAM.WebGallery.Model;
using EPAM.WebGallery.Services;
using EPAM.WebGallery.Site.Helpers;
using EPAM.WebGallery.Site.Models;

namespace EPAM.WebGallery.Site.Controllers
{
	public class HeapController : BaseController
	{
		protected ContentService ContentService;
		protected MembershipService MembershipService;
		protected UnitOfWork UnitOfWork;

		public HeapController()
		{
			UnitOfWork = new UnitOfWork();
			ContentService = new ContentService(UnitOfWork);
			MembershipService = new MembershipService(UnitOfWork);
		}

		public ActionResult Index()
		{
			IEnumerable<User> users = MembershipService.GetAllUsers();
			return View(UserBuilder.ConvertToViewModels(users));
		}

		public ActionResult Albums(string login)
		{
			try
			{
				Expect.ArgumentNotNull(login);
				IEnumerable<Album> albums = ContentService.GetAllUserAlbum(login);
				var Model = new AlbumListViewModel();
				Model.UserLogin = login;
				Model.Albums = AlbumBuilder.ConvertToViewModels(albums);
				return View(Model);
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Heap");
			}
		}

		public ActionResult Photos(string login, string albumName)
		{
			try
			{
				Expect.ArgumentNotNull(albumName);
				Album album = ContentService.GetAlbumByName(login, albumName);
				var photoList = new PhotoListViewModel();
				photoList.Photos = PhotoBuilder.BuildModels(album.Photos);
				photoList.UserName = login;
				photoList.AlbumName = albumName;
				return View(photoList);
			}
			catch (Exception)
			{
				return RedirectToAction("Index");
			}
		}

		public ActionResult Autocomplete(string query)
		{
			var al = ContentService.GetAllAlbum();
			var albums = (from u in ContentService.GetAllAlbum()
							where u.Name.Contains(query)
							select u.Name).Distinct().ToArray();

			return Json(albums, JsonRequestBehavior.AllowGet);
		}
	}
}