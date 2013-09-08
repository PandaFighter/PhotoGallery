using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPAM.WebGallery.Model;
using EPAM.WebGallery.Services;
using EPAM.WebGallery.Site.Models;

namespace EPAM.WebGallery.Site.Helpers
{
	public static class PhotoBuilder
	{
		public static IEnumerable<PhotoViewModel> BuildModels(IEnumerable<Photo> photos)
		{
			if (photos != null) return photos.Select(ConvertToViewModel).ToList();
			return null;
		}

		public static Photo ConvertToEntity(PhotoViewModel viewModel)
		{
			var photo = (new ContentService()).GetPhotoById(viewModel.Id);
			return photo;
		}

		public static PhotoViewModel ConvertToViewModel(Photo photo)
		{
			var viewModel = new PhotoViewModel
				{
					Description = photo.Desription,
					Image = "data:image/png;base64," + Convert.ToBase64String(photo.Image.ToArray()),
					ImagePreview = "data:image/png;base64," + Convert.ToBase64String(photo.ImagePreview.ToArray()), 
					Name = photo.Name,
					Id = photo.Id
				};
			return viewModel;
		}
	}
}