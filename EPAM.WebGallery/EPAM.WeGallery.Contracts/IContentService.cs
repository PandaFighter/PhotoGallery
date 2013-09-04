using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Contracts
{
	public interface IContentService : IService
	{
		//AlbumService
		Album GetAlbumByName(string user, string name);

		void DeleteAlbum(Guid id);

		IEnumerable<Album> GetAllUserAlbum(string userName);

		Album GetAlbumById(Guid id);

		void UpdateAlbum(Album album);

		//PhotoService

		//Photo CreateNewPhoto(string photoName, string albumName);

	}
}
