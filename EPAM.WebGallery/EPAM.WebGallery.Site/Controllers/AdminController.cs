using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPAM.WebGallery.Common;
using EPAM.WebGallery.Services;
using EPAM.WebGallery.Site.Helpers;
using EPAM.WebGallery.Site.Models;

namespace EPAM.WebGallery.Site.Controllers
{
    public class AdminController : BaseController
    {
		[Authorize]
		[AuthorizeUser(AccessLevel = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
			
		public ViewResult Users()
		{
			var users = (new MembershipService()).GetAllUsers();
			var userList = UserBuilder.ConvertToViewModels(users);
			var list = userList.OrderBy(model => model.Login);
			return View(list);
		}

		public ActionResult Edit(string userLogin)
		{
			try
			{
				Expect.ArgumentNotNull(userLogin);
				var user = (new MembershipService()).GetUserByLogin(userLogin);
				var userModel = UserBuilder.ConvertToViewModel(user);
				return View(userModel);
			}
			catch (Exception ex)
			{
				TempData["message"] = "User not found!";
				return RedirectToAction("Index");
			}
		}	
		[HttpPost]
		public ActionResult Edit(UserViewModel userViewModel)
		{
			try
			{
				Expect.ArgumentNotNull(userViewModel);
				var user = UserBuilder.ConvertToEntity(userViewModel);
				(new MembershipService()).UpdateUser(user);
				return RedirectToAction("Edit", userViewModel);
			}
			catch (Exception)
			{
				throw;
			}
		}
		
		public ActionResult Delete(string id)
		{
			try
			{
				Expect.ArgumentNotNull(id);
				var Id = Guid.Parse(id);
				var service = new MembershipService();
				service.DeleteUser(Id);
				TempData["message"] = "User was deleted!"; 
				return RedirectToAction("Users");
			}
			catch (Exception)
			{
				TempData["message"] = "User no found";
				return RedirectToAction("Users");
			}
		}

		public ActionResult IsAdmin()
		{
			if (string.IsNullOrEmpty(User.Identity.Name))
			{
				return View(false);
			}
			var login = User.Identity.Name;
			var user = (new MembershipService()).GetUserByLogin(login);
			if (user ==null)
			{
				return View(false);
			}
            if (user.IsInRole("Administrator"))
            {
                return View(true);
            }
            else
            {
                return View(false);
            }
		}
    }
}
