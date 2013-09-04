using System.Collections.Generic;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Data.Repositories
{
    public interface IRepository
    {
    }

	public interface IRepository<TEntity, in TKey> : IRepository where TEntity : Entity
	{
		void Create(TEntity entity);

		TEntity Get(TKey key);

		IEnumerable<TEntity> GetAll(); 

		void Update(TEntity entity);

		void Delete(TKey key);
	}
}
