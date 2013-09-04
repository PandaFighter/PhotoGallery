using System;

namespace EPAM.WebGallery.Common
{
    public static class Expect
    {
		public static void ArgumentNotNull(object value, string paramName = "")
		{
			if (value == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}
    }
}
