using System;
using System.Web.Mvc;
using DotNetOpenAuth.Messaging;
using EPAM.WebGallery.Common;
using EPAM.WebGallery.Contracts;
using EPAM.WebGallery.Data.EF;
using EPAM.WebGallery.Model;
using EPAM.WebGallery.Services;
using EPAM.WebGallery.Site.Global.OAuth;
using EPAM.WebGallery.Site.Models;
using Ninject;

namespace EPAM.WebGallery.Site.Controllers
{
	public class AccountController : BaseController
	{
		protected UnitOfWork UnitOfWork;

		private readonly IOpenIdMembershipService _openIdService;

		public AccountController()
		{
			UnitOfWork = new UnitOfWork();
			MembershipService = new MembershipService(UnitOfWork);
			_openIdService = new OpenIdService();
		}

		[Inject]
		protected IMembershipService MembershipService { get; set; }

		public T GetService<T>()
		{
			return MyDependencyResolver.Current.Resolve<T>();
		}

		public ActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public ActionResult SignIn(SignInModel person, string returnUrl)
		{
			try
			{
				Expect.ArgumentNotNull(person.Login);
				Expect.ArgumentNotNull(person.Password);
			}
			catch (Exception)
			{
				return RedirectToAction("SignIn", "Account");
			}
			User authUser = Auth.Login(person.Login, person.Password, false);
			if (authUser != null)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		//[HttpPost]
		//public ActionResult SignIn(string openid_identifier)
		//{
		//	var response = _openIdService.ValidateAtOpenIdProvider(openid_identifier);

		//	if (response != null)
		//	{
		//		return response.RedirectingResponse.AsActionResult();
		//	}

		//	return View();
		//}

		public ActionResult SignOut()
		{
			Auth.LogOut();
			return RedirectToAction("Index", "Home");
		}

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterModel person)
		{
			if (ModelState.IsValid)
			{
				try
				{
					User user = MembershipService.CreateUser(person.Login, person.Email, person.Password);
					MembershipService.AddUserToRole(user.Id,
					                                user.Login.Equals("admin") ? Role.Names.Administrator : Role.Names.Membership);
					return RedirectToAction("SignIn");
				}
				catch (Exception ex)
				{
					TempData["message"] = "User already exists";
					return RedirectToAction("Register");
				}
			}
			return View();
		}


		public ViewResult Profile()
		{
			User user = MembershipService.GetUserByLogin(User.Identity.Name);
			return View();
		}
	}
}