using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.WebGallery.Site.Models
{
	public class AlbumListViewModel
	{
		public string UserLogin { get; set; }

		public IEnumerable<AlbumViewModel> Albums { get; set; }
	}
}