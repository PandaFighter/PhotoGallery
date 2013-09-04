using System;
using System.Collections.Generic;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Contracts
{
	public interface IMembershipService : IService
	{

		User CreateUser(string login, string email, string password);
		
		User GetUserById(Guid id);

		User GetUserByEmail(string email);

		User GetUserByLogin(string login);
		
		bool UserExistsByEmail(string email);

		bool UserExistsByLogin(string login);

		IEnumerable<User> GetAllUsers();

		void DeleteUser(Guid id);

		bool LoginUser(string login, string password);

		void LogoutUser(Guid id);

		void ChangePassword(Guid id, string newPassword, string oldPassword);

		void ChangeEmail(Guid id, string newEmail);

		void AddUserToRole(Guid id, string roleName);

		void RemoveUserFromRole(Guid id, string roleName);

		void UpdateUser(User user);

		Role GetRoleByName(string roleName);

	}
}
