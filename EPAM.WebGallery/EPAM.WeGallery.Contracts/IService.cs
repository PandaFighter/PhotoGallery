using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Contracts
{
    public interface IService
    {
    }
	public interface IService<TEntity> where TEntity : Entity
	{
	}
}
