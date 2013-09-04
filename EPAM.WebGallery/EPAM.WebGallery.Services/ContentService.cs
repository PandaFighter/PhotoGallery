using System;
using System.Collections.Generic;
using System.Linq;
using EPAM.WebGallery.Common;
using EPAM.WebGallery.Contracts;
using EPAM.WebGallery.Contracts.Exceptions;
using EPAM.WebGallery.Data.EF;
using EPAM.WebGallery.Data.EF.Repositories;
using EPAM.WebGallery.Data.Repositories;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Services
{
	public class ContentService : BaseService, IContentService
	{
		protected IAlbumRepository AlbumRepository;

		protected IPhotoRepository PhotoRepository;

		protected IUserRepository UserRepository;

		public ContentService(UnitOfWork unitOfWork) : base(unitOfWork)
		{
			AlbumRepository = new AlbumRepository(unitOfWork);
			PhotoRepository = new PhotoRepository(unitOfWork);
			UserRepository = new UserRepository(unitOfWork);
		}

		public ContentService()
		{
			AlbumRepository = new AlbumRepository(new UnitOfWork());
			PhotoRepository = new PhotoRepository(new UnitOfWork());
			UserRepository = new UserRepository(new UnitOfWork());
		}

		#region AlbumService

		public void DeleteAlbum(Guid id)
		{
			Expect.ArgumentNotNull(id);
			IAlbumRepository repository = AlbumRepository;
			Album album = GetAlbumById(id);
			PhotoRepository.DeleteAll(album.Photos);
			if (GetAlbumById(id) != null)
			{
				repository.Delete(id);
			}
		}

		public IEnumerable<Album> GetAllAlbum()
		{
			var albums = AlbumRepository.GetAll();
			return albums;
		}

		public Album GetAlbumByName(string login, string albumName)
		{
			Expect.ArgumentNotNull(login);
			Expect.ArgumentNotNull(albumName);
			User user = (new MembershipService().GetUserByLogin(login));
			try
			{
				return user.Albums.SingleOrDefault(album => album.Name.Equals(albumName, StringComparison.OrdinalIgnoreCase));
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Album GetAlbumById(Guid id)
		{
			try
			{
				Expect.ArgumentNotNull(id);
			}
			catch (Exception)
			{
				
				throw;
			}
			return AlbumRepository.GetById(id);
		}



		public IEnumerable<Album> GetAllUserAlbum(string login)
		{
			Expect.ArgumentNotNull(login);
			User user = UserRepository.GetByLogin(login);
			if (user == null)
				throw new UserNotFoundException();
			return user.Albums;
		}

		public void UpdateAlbum(Album album)
		{
			Expect.ArgumentNotNull(album);
			AlbumRepository.Update(album);
		}

		public Album CreateNewAlbum(string userName, string albumName, string description = "")
		{
			Expect.ArgumentNotNull(albumName);
			Expect.ArgumentNotNull(userName);
			var service = new MembershipService();
			User user = service.GetUserByLogin(userName);
			if (GetAlbumByName(userName, albumName) != null)
			{
				throw new DuplicateAlbumException {AlbumName = albumName};
			}
			var album = new Album {Name = albumName, Description = description};
			user.Albums.Add(album);
			album.User = user;
			service.UpdateUser(user);
			return album;
		}

		#endregion 

		#region PhotoService

		public void CreateNewPhoto(Photo photo, string albumName, string login)
		{
			Expect.ArgumentNotNull(photo);
			Expect.ArgumentNotNull(albumName);
			Expect.ArgumentNotNull(login);
		    var service = new MembershipService(UnitOfWork);
			User user = service.GetUserByLogin(login);
			Album album = user.Albums.FirstOrDefault(x => x.Name == albumName);
			if (album != null)
			{
				album.Photos.Add(photo);
				photo.Album = album;
			}
		   // UnitOfWork.Commit();
		    service.UpdateUser(user);
		    //var album = GetAlbumByName(login, albumName);
		    //album.Photos.Add(photo);
		    //UpdateAlbum(album);
		}

		public Photo GetPhotoById(Guid id)
		{
			Expect.ArgumentNotNull(id);
			return PhotoRepository.GetById(id);
		}

		public void DeletePhoto(Guid id)
		{
			Expect.ArgumentNotNull(id);
			PhotoRepository.Delete(id);
		}

		public void UpdatePhoto(Photo photo)
		{
			Expect.ArgumentNotNull(photo);
			PhotoRepository.Update(photo);
		}

		#endregion
	}
}