using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EPAM.WebGallery.Model
{
	public class Tag : Entity<Guid>
	{
		private ICollection<Photo> _photos;

		public Tag()
		{
			Id = Guid.NewGuid();
		}

		public string Name { get; set; }

		public virtual ICollection<Photo> Photos
		{
			get { return _photos ?? (_photos = new Collection<Photo>()); }
			set { _photos = value; }
		}
	}
}