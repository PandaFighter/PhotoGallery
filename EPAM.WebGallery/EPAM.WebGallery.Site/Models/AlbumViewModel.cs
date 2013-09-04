using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Site.Models
{
	public class AlbumViewModel
	{
		public string Name { get; set; }

		[HiddenInput(DisplayValue = false)]
		public Guid Id { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		
		[HiddenInput(DisplayValue = false)]
		public ICollection<Photo> Photos { get; set; }

		public AlbumViewModel()
		{
			
		}
	}
}