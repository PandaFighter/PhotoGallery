using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPAM.WebGallery.Model;
using EPAM.WebGallery.Services;
using EPAM.WebGallery.Site.Models;

namespace EPAM.WebGallery.Site.Helpers
{
	public static class UserBuilder
	{
		public static User ConvertToEntity(UserViewModel userViewModel)
		{
			var id = Guid.Parse(userViewModel.Id);
			var user = (new MembershipService()).GetUserById(id);
			user.Email = userViewModel.Email;
			user.Login = userViewModel.Login;
			user.Password = userViewModel.Password;
			return user;
		}

		public static UserViewModel ConvertToViewModel(User user)
		{
			var model = new UserViewModel
				{
					Login = user.Login,
					Password = user.Password,
					Email = user.Email,
					Id = user.Id.ToString(),
					Albums = user.Albums
				};
			return model;
		}

		public static IEnumerable<UserViewModel> ConvertToViewModels(IEnumerable<User> users)
		{
			return users.Select(ConvertToViewModel).ToList();
		}
	}
}