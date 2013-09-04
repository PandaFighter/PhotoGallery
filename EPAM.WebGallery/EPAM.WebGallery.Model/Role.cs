using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Model
{
	public class Role : Entity<Guid>
	{
		public static class Names
		{
			public const string Administrator = "Administrator";
			public const string Moderator = "Moderator";
			public const string Membership = "Membership";
		}

		private ICollection<User> _users;

		public string Name { get; set; }

		public Role()
		{
			Id = Guid.NewGuid();
		}

		public virtual ICollection<User> Users
		{
			get { return _users ?? (_users = new Collection<User>()); }
			set { _users = value; }
		}
	}
}
