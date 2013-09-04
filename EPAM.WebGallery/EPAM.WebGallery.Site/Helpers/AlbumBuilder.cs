using System.Collections.Generic;
using System.Linq;
using EPAM.WebGallery.Common;
using EPAM.WebGallery.Model;
using EPAM.WebGallery.Services;
using EPAM.WebGallery.Site.Models;

namespace EPAM.WebGallery.Site.Helpers
{
	public static class AlbumBuilder
	{
		public static AlbumViewModel ConvertToViewModel(Album album)
		{
			var viewAlbum = new AlbumViewModel
				{
					Name = album.Name,
					Id = album.Id,
					Description = album.Description,
					Photos = album.Photos
				};
			return viewAlbum;
		}

		public static Album ConvertToEntity(AlbumViewModel viewAlbum)
		{
			Expect.ArgumentNotNull(viewAlbum);
			var service = new ContentService();
			Album album = service.GetAlbumById(viewAlbum.Id);
			if (album != null)
			{
				album.Name = viewAlbum.Name;
				album.Description = viewAlbum.Description;
			}
			return album;
		}

		public static IEnumerable<AlbumViewModel> ConvertToViewModels(IEnumerable<Album> albums)
		{
			return albums.Select(ConvertToViewModel).ToList();
		}
	}
}