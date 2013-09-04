using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Data.Repositories
{
	public interface IPhotoRepository : IRepository<Photo, Guid>
	{
		Photo GetById(Guid id);

		void DeleteAll(ICollection<Photo> photos);
	}
}
