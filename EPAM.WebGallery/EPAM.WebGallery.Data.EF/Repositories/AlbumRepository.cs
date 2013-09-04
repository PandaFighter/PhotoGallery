using System;
using System.Linq;
using EPAM.WebGallery.Data.Repositories;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Data.EF.Repositories
{
	public class AlbumRepository : EntityRepository<Album>, IAlbumRepository
	{
		public AlbumRepository(UnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public AlbumRepository()
		{
		}

		public void Delete(Guid id)
		{
			base.Delete(id);
		}

		public Album Get(Guid id)
		{
			return base.Get(id);
		}

		public void Update(Album album)
		{
			base.Update(album, album.Id);
		}

		public Album GetById(Guid id)
		{
			return Context.Albums.FirstOrDefault(album => album.Id == id);
		}
	}
}