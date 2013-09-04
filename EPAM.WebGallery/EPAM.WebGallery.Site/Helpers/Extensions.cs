using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace EPAM.WebGallery.Site.Helpers
{
	public static class Extensions
	{
		public static bool IsSuccessful(this IAuthenticationResponse response)
		{
			return response != null && response.Status == AuthenticationStatus.Authenticated;
		}
	}
}