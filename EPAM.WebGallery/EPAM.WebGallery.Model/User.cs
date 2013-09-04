using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Model
{
	public class User : Entity<Guid>
	{
		public string Login { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		private ICollection<Role> _roles;
		private ICollection<Album> _albums;
		public Profile Profile { get; set; }

		public bool IsLogged { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsBlocked { get; set; }

		public DateTime? CreatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public DateTime? LoggedDate { get; set; }

		public virtual ICollection<Album> Albums
		{
			get { return _albums ?? (_albums = new Collection<Album>()); }
			set { _albums = value; }
		}
		public virtual ICollection<Role> Roles
		{
			get { return _roles ?? (_roles = new Collection<Role>()); }
			set { _roles = value; }
		}

		public User()
		{
			Id = Guid.NewGuid();
			CreatedDate = DateTime.UtcNow;
		}

		public bool IsInRole(string roleName)
		{
			return Roles.Any(x => x.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
		}

		public bool VerifyPassword(string password)
		{
			return Password.Equals(password, StringComparison.Ordinal);
		}
}
}
