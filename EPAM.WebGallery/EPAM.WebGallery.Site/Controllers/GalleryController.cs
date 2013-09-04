using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using EPAM.WebGallery.Common;
using EPAM.WebGallery.Contracts.Exceptions;
using EPAM.WebGallery.Data.EF;
using EPAM.WebGallery.Model;
using EPAM.WebGallery.Services;
using EPAM.WebGallery.Site.Helpers;
using EPAM.WebGallery.Site.Models;

namespace EPAM.WebGallery.Site.Controllers
{

	//[OutputCache( Duration = 3600)]
	public class GalleryController : BaseController
	{
		private static readonly HashSet<byte[]> Images = new HashSet<byte[]>();

		protected ContentService ContentService;

		protected MembershipService MembershipService;

		protected UnitOfWork UnitOfWork;

		public GalleryController()
		{
			UnitOfWork = new UnitOfWork();
			ContentService = new ContentService(UnitOfWork);
			MembershipService = new MembershipService(UnitOfWork);
		}

		public ActionResult Index()
		{
			string login = User.Identity.Name;
			User user = MembershipService.GetUserByLogin(login);
			UserViewModel userViewModel = UserBuilder.ConvertToViewModel(user);
			return View(userViewModel);
		}

		public ActionResult Albums()
		{
			Response.Cache.SetCacheability(HttpCacheability.Public);
			string login = User.Identity.Name;
			User user = MembershipService.GetUserByLogin(login);
			UserViewModel userViewModel = UserBuilder.ConvertToViewModel(user);
			return View(userViewModel);
		}

		[HttpGet]
		public ActionResult Album(string name)
		{
			try
			{
				Expect.ArgumentNotNull(name);
			}
			catch (Exception)
			{
				return RedirectToAction("Index");
			}
			string login = User.Identity.Name;
			User user = MembershipService.GetUserByLogin(login);
			Album showAlbum = user.Albums.FirstOrDefault(album => album.Name == name);
			if (showAlbum == null)
			{
				return RedirectToAction("Index");
			}
			AlbumViewModel albumModel = AlbumBuilder.ConvertToViewModel(showAlbum);
			return View(albumModel);
		}

		public ActionResult EditAlbum(Guid id)
		{
			Album album = ContentService.GetAlbumById(id);
			AlbumViewModel albumModel = AlbumBuilder.ConvertToViewModel(album);
			return View(albumModel);
		}

		[HttpPost]
		public ActionResult EditAlbum(AlbumViewModel viewAlbum)
		{
			Album album = ContentService.GetAlbumByName(User.Identity.Name, viewAlbum.Name);
			if (album != null && album.Id != viewAlbum.Id)
			{
				TempData["message"] = "Album " + viewAlbum.Name + " already exists!";
				return View(viewAlbum);
			}
			album = AlbumBuilder.ConvertToEntity(viewAlbum);
			ContentService.UpdateAlbum(album);
			TempData["message"] = "Album " + album.Name + " has been saved";
			return RedirectToAction("Index");
		}

		public ActionResult CreateAlbum(AlbumViewModel albumViewModel)
		{
			try
			{
				Album album = ContentService.CreateNewAlbum(User.Identity.Name, albumViewModel.Name, albumViewModel.Description);
				TempData["message"] = "Album " + album.Name + " has been saved!";
			}
			catch (DuplicateAlbumException ex)
			{
				TempData["message"] = "The album with name " + ex.AlbumName + " already exists!";
				return RedirectToAction("Albums");
			}
			return RedirectToAction("Albums");
		}

		public ActionResult DeleteAlbum(Guid id)
		{
			ContentService.DeleteAlbum(id);
			//TempData["message"] = "Album " + album.Name + " was deleted!";
			ViewBag.Message = "blablalbs";
			return RedirectToAction("Albums");
		}

		public ActionResult Photos(string album)
		{
				Expect.ArgumentNotNull(album);
				string login = User.Identity.Name;
				User user = MembershipService.GetUserByLogin(login);
				Album showAlbum = user.Albums.FirstOrDefault(alb => alb.Name == album);
				if (showAlbum == null)
				{
					return RedirectToAction("Index");
				}
				IEnumerable<PhotoViewModel> photos = PhotoBuilder.BuildModels(showAlbum.Photos);
				ViewBag.Album = album;

			return View(photos);
		}

		public ActionResult AddPhoto(string album, HttpPostedFileBase image)
		{
			try
			{
				Expect.ArgumentNotNull(image);
				Expect.ArgumentNotNull(album);
			}
			catch (Exception)
			{
				TempData["message"] = "Error";
				return RedirectToAction("Index");
			}
			var photo = new Photo();
		
			if (image != null)
			{
				photo.Name = image.FileName;
				photo.Image = new byte[image.ContentLength];
				photo.ImageType = image.ContentType;
				image.InputStream.Read(photo.Image, 0, image.ContentLength);
				ContentService.CreateNewPhoto(photo, album, User.Identity.Name);
			}
			return RedirectToAction("Album", new {name = album});
		}

		public ActionResult DeletePhoto(string Id, string name)
		{
			Guid id = Guid.Parse(Id);
			ContentService.DeletePhoto(id);
			return RedirectToAction("Photos", new {album = name});
		}

		public FileContentResult GetImage(Guid id)
		{
			var ser = new ContentService();
			Photo photo = ser.GetPhotoById(id);
			return File(photo.Image, photo.ImageType);
		}

		public ActionResult UploadImage(HttpPostedFileBase[] files)
		{
			if (files != null)
			{
				Images.Clear();
				foreach (HttpPostedFileBase file in files)
				{
					var stream = new MemoryStream();
					file.InputStream.CopyTo(stream);
					Images.Add(stream.GetBuffer());
				}
			}
			return Json(true);
		}

		public ViewResult ShowUploadImage()
		{
			var files = new string[Images.Count];
			int i = 0;
			foreach (var image in Images)
			{
				files[i++] = "data:image/png;base64," + Convert.ToBase64String(image);
			}
			return View(files);
		}

		public ActionResult EditPhoto(string id)
		{
			Guid Id;
			try
			{
				Id = Guid.Parse(id);
				Photo photo = ContentService.GetPhotoById(Id);
				var model = PhotoBuilder.ConvertToViewModel(photo);
				return View(model);
			}
			catch (Exception)
			{
				return RedirectToAction("Albums");
			}
		}

		[HttpPost]
		public ActionResult EditPhoto(PhotoViewModel model)
		{
			if (model != null)
			{
				Photo photo = ContentService.GetPhotoById(model.Id);
				var tag = new Tag {Name = model.Tag};
				photo.Name = model.Name;
				photo.Desription = model.Description;
				
				ContentService.UpdatePhoto(photo);
			}
			return RedirectToAction("EditPhoto", new {model});
			//return Json( new { url = Url.Action("EditPhoto", new {model})});
		}
	}
}