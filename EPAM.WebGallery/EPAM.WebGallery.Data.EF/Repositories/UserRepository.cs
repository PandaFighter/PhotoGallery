using System;
using System.Linq;
using EPAM.WebGallery.Data.Repositories;
using EPAM.WebGallery.Data.UnitOfWork;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Data.EF.Repositories
{
	public class UserRepository : EntityRepository<User>, IUserRepository
	{
		public UserRepository(UnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public void Delete(Guid id)
		{
			base.Delete(id);
		}

		public User Get(Guid id)
		{
			return base.Get(id);
		}

		public void Update(User user)
		{
			base.Update(user, user.Id);
			Context.SaveChanges();
		}

		public User GetByEmail(string email)
		{
			try
			{
				return Context.Users.SingleOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public User GetByLogin(string login)
		{
			try
			{
				return Context.Users.SingleOrDefault(user => user.Login.Equals(login, StringComparison.Ordinal));
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}