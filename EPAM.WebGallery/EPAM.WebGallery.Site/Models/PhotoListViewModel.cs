using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.WebGallery.Site.Models
{
	public class PhotoListViewModel
	{
		public string UserName { get; set; }

		public string AlbumName { get; set; }

		public IEnumerable<PhotoViewModel> Photos { get; set; }

	}
}