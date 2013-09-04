using System.Configuration;
using EPAM.WebGallery.Data.UnitOfWork;

namespace EPAM.WebGallery.Data.EF
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly SiteContext _context;

		public UnitOfWork()
		{
			string conString = ConfigurationManager.ConnectionStrings[1].ConnectionString;
			_context = new SiteContext(conString);
		}

		public SiteContext Context
		{
			get { return _context; }
		}

		public void Commit()
		{
			_context.SaveChanges();
		}
	}
}