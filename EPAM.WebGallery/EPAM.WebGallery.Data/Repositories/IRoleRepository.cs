using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Data.Repositories
{
	public interface IRoleRepository : IRepository<Role,Guid>
	{
		Role GetByName(string name);
	}
}
