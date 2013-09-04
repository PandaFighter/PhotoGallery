using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Site.Global.Auth
{
	public interface IUserProvider
	{
		User User { get; set; }
	}
}