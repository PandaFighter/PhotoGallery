using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using EPAM.WebGallery.Site.Helpers;

namespace EPAM.WebGallery.Site.Global.OAuth
{
	public class OpenIdService : IOpenIdMembershipService
	{
		public readonly OpenIdRelyingParty openId;

		public OpenIdService()
		{
			openId = new OpenIdRelyingParty();
		}

		public IAuthenticationRequest ValidateAtOpenIdProvider(string openIdIdentifier)
		{
			IAuthenticationRequest openIdRequest = openId.CreateRequest(Identifier.Parse(openIdIdentifier));

			var fields = new ClaimsRequest {
				Email = DemandLevel.Require,
				FullName = DemandLevel.Require,
				Nickname = DemandLevel.Require
			};

			openIdRequest.AddExtension(fields);
			return openIdRequest;
		}

		public OpenIdUser GetUser()
		{
			OpenIdUser user = null;
			IAuthenticationResponse openIdResponce = openId.GetResponse();

			if (openIdResponce.IsSuccessful())
			{
				user = ResponseIntoUser(openIdResponce);
			}

			return user;
		}

		private OpenIdUser ResponseIntoUser(IAuthenticationResponse response)
		{
			OpenIdUser user = null;
			var claimResponseUntrusted = response.GetUntrustedExtension<ClaimsResponse>();
			var claimResponse = response.GetExtension<ClaimsResponse>();

			if (claimResponse != null)
			{
				user = new OpenIdUser(claimResponse, response.ClaimedIdentifier);
			}
			else if (claimResponseUntrusted != null)
			{
				user = new OpenIdUser(claimResponseUntrusted, response.ClaimedIdentifier);
			}

			return user;
		}
	}
}