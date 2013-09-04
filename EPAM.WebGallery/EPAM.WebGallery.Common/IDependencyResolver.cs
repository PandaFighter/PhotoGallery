namespace EPAM.WebGallery.Common
{
	public interface IDependencyResolver
	{
		T Resolve<T>();
	}
}
