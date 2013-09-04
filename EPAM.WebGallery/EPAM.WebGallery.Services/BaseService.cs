using EPAM.WebGallery.Contracts;
using EPAM.WebGallery.Data.EF;
using EPAM.WebGallery.Data.UnitOfWork;

namespace EPAM.WebGallery.Services
{
	public class BaseService : IService
	{
		public BaseService(UnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}

		public BaseService()
		{
			UnitOfWork = new UnitOfWork();
		}

		public UnitOfWork UnitOfWork { get; set; }
	}
}