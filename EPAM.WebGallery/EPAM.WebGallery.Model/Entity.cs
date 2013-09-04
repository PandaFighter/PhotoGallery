using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Model
{
	public class Entity
	{
		public Guid Id { get; set; }
	}

	public class Entity<T> : Entity
	{
		//public T Id { get; set; }
	}
}
