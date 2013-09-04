using System;
using System.Collections.Generic;
using EPAM.WebGallery.Common;
using EPAM.WebGallery.Contracts;
using EPAM.WebGallery.Contracts.Exceptions;
using EPAM.WebGallery.Data.EF;
using EPAM.WebGallery.Data.EF.Repositories;
using EPAM.WebGallery.Data.Exceptions;
using EPAM.WebGallery.Data.Repositories;
using EPAM.WebGallery.Data.UnitOfWork;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Services
{
	public class MembershipService : BaseService, IMembershipService
	{
		protected IRoleRepository RoleRepository;
		protected IUserRepository UserRepository;

		public MembershipService(UnitOfWork unitOfWork) : base(unitOfWork)
		{
			UserRepository = new UserRepository(unitOfWork);
			RoleRepository = new RoleRepository(unitOfWork);
		}

		public MembershipService()
		{
			UserRepository = new UserRepository(new UnitOfWork());
			RoleRepository = new RoleRepository(new UnitOfWork());
		}

		#region UserService

		public User CreateUser(string login, string email, string password)
		{
			Expect.ArgumentNotNull(login);
			Expect.ArgumentNotNull(email);
			Expect.ArgumentNotNull(password);
			User user;
			if (GetUserByLogin(login) != null)
			{
				throw new DuplicateLoginException {Login = login};
			}
			try
			{
				user = new User {Login = login, Email = email, Password = password};
				UserRepository.Create(user);
			}
			catch (Exception exception)
			{
				throw new MembershipServiceException(exception);
			}
			return user;
		}

		public void DeleteUser(Guid id)
		{
			Expect.ArgumentNotNull(id);
			User user;
			try
			{
				user = UserRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw new DataException(ex.Message);
			}
			if (user == null)
			{
				throw new UserNotFoundException();
			}
			UserRepository.Delete(id);
		}

		public User GetUserByLogin(string login)
		{
			Expect.ArgumentNotNull(login);
			try
			{
				return UserRepository.GetByLogin(login);
			}
			catch (Exception ex)
			{
				throw new MembershipServiceException(ex);
			}
		}

		public User GetUserById(Guid id)
		{
			Expect.ArgumentNotNull(id);
			User user;
			try
			{
				user = UserRepository.Get(id);
			}
			catch (Exception ex)
			{
				throw new MembershipServiceException(ex);
			}
			if (user == null)
			{
				throw new UserNotFoundException {UserId = id};
			}
			return user;
		}

		public void UpdateUser(User user)
		{
			Expect.ArgumentNotNull(user);
			try
			{
				UserRepository.Update(user);
			}
			catch (Exception exception)
			{
				throw new MembershipServiceException(exception);
			}
		}

		public IEnumerable<User> GetAllUsers()
		{
			try
			{
				return UserRepository.GetAll();
			}
			catch (Exception ex)
			{
				throw new MembershipServiceException(ex);
			}
		}

		public void ChangeEmail(Guid id, string newEmail)
		{

			User dupUser;
			try
			{
				dupUser = GetUserByEmail(newEmail);
				Expect.ArgumentNotNull(id);
				Expect.ArgumentNotNull(newEmail);
			}
			catch (Exception exception)
			{
				throw new MembershipServiceException(exception);
			}
			if (dupUser != null)
				throw new DuplicateEmailException {Email = newEmail};
			User user = GetUserById(id);
			user.Email = newEmail;
			UpdateUser(user);
		}

		public void ChangePassword(Guid id, string oldPassword, string newPassword)
		{
			Expect.ArgumentNotNull(id);
			Expect.ArgumentNotNull(oldPassword);
			Expect.ArgumentNotNull(newPassword);
			User user = GetUserById(id);
			if (!user.VerifyPassword(oldPassword))
				throw new WrongPasswordException {Password = oldPassword};
			user.Password = newPassword;
			UpdateUser(user);
		}

		public User GetUserByEmail(string email)
		{
			Expect.ArgumentNotNull(email);
			User user;
			try
			{
				user = UserRepository.GetByEmail(email);
			}
			catch (Exception ex)
			{
				throw new MembershipServiceException(ex);
			}
			if (user == null)
				throw new UserNotFoundException();
			return user;
		}

		public bool LoginUser(string login, string password)
		{
			Expect.ArgumentNotNull(login);
			Expect.ArgumentNotNull(password);
			User user = GetUserByLogin(login);
			if (user == null)
				return false;
			if (!user.VerifyPassword(password))
				return false;
			user.IsLogged = true;
			user.LoggedDate = DateTime.UtcNow;
			UpdateUser(user);
			return true;
		}

		public void LogoutUser(Guid id)
		{
			Expect.ArgumentNotNull(id);
			User user = GetUserById(id);
			user.IsLogged = false;
			UpdateUser(user);
		}


		public bool UserExistsByEmail(string email)
		{
			Expect.ArgumentNotNull(email);
			User user = UserRepository.GetByEmail(email);
			if (user == null)
				return false;
			return true;
		}

		public bool UserExistsByLogin(string login)
		{
			Expect.ArgumentNotNull(login);
			User user = UserRepository.GetByLogin(login);
			if (user == null)
				return false;
			return true;
		}

		#endregion

		#region RoleService

		public Role GetRoleByName(string roleName)
		{
			Expect.ArgumentNotNull(roleName);
			Role role;
			try
			{
				role = RoleRepository.GetByName(roleName);
			}
			catch (Exception ex)
			{
				throw new DataException(ex.Message);
			}
			if (role == null)
				throw new RoleNotFoundException {RoleName = roleName};
			return role;
		}

		public void AddUserToRole(Guid id, string roleName)
		{
			Expect.ArgumentNotNull(id);
			Expect.ArgumentNotNull(roleName);
			try
			{
				User user = GetUserById(id);
				Role role = GetRoleByName(roleName);
				if (role != null)
				{
					user.Roles.Add(role);
					UpdateUser(user);
				}
			}
			catch (Exception)
			{
				throw new MembershipServiceException();
			}
		}

		public void RemoveUserFromRole(Guid id, string roleName)
		{
			Expect.ArgumentNotNull(id);
			Expect.ArgumentNotNull(roleName);
			try
			{
				User user = GetUserById(id);
				Role role = GetRoleByName(roleName);
				user.Roles.Remove(role);
				UpdateUser(user);
			}
			catch (Exception exception)
			{
				throw new MembershipServiceException(exception);
			}
		}

		#endregion
	}
}