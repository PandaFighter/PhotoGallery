using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.WebGallery.Site.Models
{
	public class NewPhotoModel
	{
		public string Album { get; set; }

		public HttpPostedFileBase Image { get; set; }

		public string Description { get; set; }

		public string[] Tags { get; set; }
	}
}