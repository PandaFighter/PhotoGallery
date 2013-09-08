using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Model
{
	public class Album : Entity<Guid>
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public virtual User User { get; set; }

		//new
		public Guid UserId { get; set; }

		public DateTime? CreatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public DateTime? ChangedDate { get; set; }

		private ICollection<Photo> _photos;

		public Album()
		{
			Id = Guid.NewGuid();
			CreatedDate = DateTime.UtcNow;
		}
		public virtual ICollection<Photo> Photos
		{
			get { return _photos ?? (_photos = new Collection<Photo>()); }
			set { _photos = value; }
		}
	}
}
