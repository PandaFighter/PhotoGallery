using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.WebGallery.Common;
using EPAM.WebGallery.Data.Repositories;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Data.EF.Repositories
{
	public class PhotoRepository : EntityRepository<Photo>, IPhotoRepository
	{
		public PhotoRepository(UnitOfWork unitOfWork) : base(unitOfWork)
		{
			
		}

		public Photo GetById(Guid id)
		{
			Expect.ArgumentNotNull(id);
			return Context.Photos.FirstOrDefault(photo => photo.Id == id);
		}

		public void DeleteAll(ICollection<Photo> photos )
		{
			Context.Photos.RemoveRange(photos);
			Context.SaveChanges();
		}

		public void Delete(Guid id)
		{
			base.Delete(id);
		}

		public Photo Get(Guid id)
		{
			return base.Get(id);
		}

		public void Update(Photo photo)
		{
			base.Update(photo, photo.Id);
		}

	}
}
