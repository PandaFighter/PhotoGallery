using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Site.Models
{
	public class PhotoViewModel
	{

		public string Name { get; set; }

		[HiddenInput]
		public Guid Id { get; set; }

		public string Description { get; set; }

		public string AlbumName { get; set; }

		public string Image { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ImageMimeType { get; set; }

		public string Tag { get; set; }

		public PhotoViewModel()
		{
			
		}
	}
}