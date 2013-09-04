using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Model
{
	public abstract class EntitySummary
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public DateTime? CreatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public DateTime? ChangedDate { get; set; }
	}
}
