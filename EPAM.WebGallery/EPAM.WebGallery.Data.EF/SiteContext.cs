using System.Data.Entity;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Data.EF
{
	public class SiteContext : DbContext
	{
		public SiteContext(string conString) : base(conString)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Profile> Profiles { get; set; }
		public DbSet<Album> Albums { get; set; }
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Tag> Tags { get; set; }
	}
}