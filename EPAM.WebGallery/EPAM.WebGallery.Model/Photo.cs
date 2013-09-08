using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EPAM.WebGallery.Model
{
	public class Photo : Entity<Guid>
	{
		private ICollection<Comment> _comments;
		private ICollection<Like> _likes;

		public Photo()
		{
			Id = Guid.NewGuid();
			CreatedDate = DateTime.UtcNow;
		}

		public string Name { get; set; }
		public string Desription { get; set; }

		public byte[] Image { get; set; }
		public byte[] ImagePreview { get; set; }

		public string ImageType { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }

		private ICollection<Tag> Tags { get; set; }

		public Album Album { get; set; }
		public Guid AlbumId { get; set; }

		public virtual ICollection<Like> Likes
		{
			get { return _likes ?? (_likes = new Collection<Like>()); }
			set { _likes = value; }
		}

		public virtual ICollection<Comment> Comment
		{
			get { return _comments ?? (_comments = new Collection<Comment>()); }
		}
	}
}