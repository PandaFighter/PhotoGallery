using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Model
{
	public class Comment : Entity<Guid>
	{
		public string Text { get; set; }
		public User User { get; set; }
		public DateTime? CreatedDate { get; set; }
		public virtual Photo Photo { get; set; }
		public Comment()
		{
			Id = Guid.NewGuid();
		}
	}
}
