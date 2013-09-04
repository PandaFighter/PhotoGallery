using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Site.Models
{
	public class UserViewModel
	{
		public string Id { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string Email { get; set; }

		private ICollection<Album> _albums; 

		public ICollection<Album> Albums
		{
			get { return _albums ?? (_albums = new Collection<Album>()); }
			set { _albums = value; }
		}

		public UserViewModel()
		{
			
		}
	}
}