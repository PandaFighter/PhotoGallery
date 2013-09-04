using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.WebGallery.Data.Repositories;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Data.EF.Repositories
{
	public class RoleRepository : EntityRepository<Role>, IRoleRepository
	{

		public RoleRepository(UnitOfWork unitOfWork) : base(unitOfWork)
		{
			
		}
		public void Delete(Guid id)
		{
			base.Delete(id);
		}

		public Role Get(Guid id)
		{
			return base.Get(id);
		}

		public void Update(Role role)
		{
			base.Update(role, role.Id);
		}
		public Role GetByName(string name)
		{
			return Context.Roles.SingleOrDefault(role => role.Name.Equals(name));

		}

	}
}
