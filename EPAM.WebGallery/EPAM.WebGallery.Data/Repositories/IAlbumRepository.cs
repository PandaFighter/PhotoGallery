using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Data.Repositories
{
	public interface IAlbumRepository : IRepository<Album,Guid>
	{
		Album GetById(Guid id);
	}
}
