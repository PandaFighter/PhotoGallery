using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.WebGallery.Common
{
	public static class MyDependencyResolver
	{
		public static IDependencyResolver Current { get; set; }

		static MyDependencyResolver()
		{
		}

		public static void SetResolver(IDependencyResolver resolver)
		{
			Expect.ArgumentNotNull(resolver);
			Current = resolver;
		}
	}
}
