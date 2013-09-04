using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace EPAM.WebGallery.Site.Models
{
	public class RegisterModel
	{
		[Required(ErrorMessage = "Please, enter your login")]
		[StringLength(10, MinimumLength = 5, ErrorMessage = "Value must be between 5 to 10 characters")]
		[Display(Name = "User name")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Please, enter your e-mail")]
		[Display(Name = "Email")]
		[EmailAddress(ErrorMessage = "Incorrect e-mail")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Enter your password")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[StringLength(15, MinimumLength = 3, ErrorMessage = "Value must be between 3 to 15 characters")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Enter confirm password")]
		[Display(Name = "Подтверждение пароля")]
		[System.Web.Mvc.Compare("Password", ErrorMessage = "Пароли не совпадают")]
		public string ConfirmPassword { get; set; }
	}

	public class SignInModel
	{
		[Required(ErrorMessage = "Enter your login")]
		[Display(Name = "User name")]
		[StringLength(10, MinimumLength = 5, ErrorMessage = "Value must be between 5 to 10 characters")]
		public string Login { get; set; }

		[Required(ErrorMessage = "Password")]
		[DataType(DataType.Password)]
		[StringLength(15, MinimumLength = 3, ErrorMessage = "Value must be between 3 to 15 characters")]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}