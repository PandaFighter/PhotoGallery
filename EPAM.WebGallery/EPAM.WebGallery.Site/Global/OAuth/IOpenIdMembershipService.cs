using DotNetOpenAuth.OpenId.RelyingParty;

namespace EPAM.WebGallery.Site.Global.OAuth
{
	internal interface IOpenIdMembershipService
	{
		IAuthenticationRequest ValidateAtOpenIdProvider(string openIdIdentifier);

		OpenIdUser GetUser();
	}
}