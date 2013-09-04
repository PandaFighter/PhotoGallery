using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Model
{
	public class Like : Entity<Guid>
	{
		public virtual User User { get; set; }
		public DateTime? SetDate { get; set; }
		public virtual Photo Photo { get; set; }
		public int? Match { get; set; }
		public Like()
		{
			Id = Guid.NewGuid();
		}
		//ссылка на юзера
		//время
		//фото
		//поле оценки
	}
}
