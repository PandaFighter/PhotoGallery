using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.WebGallery.Common;
using EPAM.WebGallery.Data.Exceptions;
using EPAM.WebGallery.Data.UnitOfWork;
using EPAM.WebGallery.Model;

namespace EPAM.WebGallery.Data.EF.Repositories
{
	public class EntityRepository<TEntity> where TEntity : Entity
	{
		protected const string DbErrorMessage = "Problem with database";

		protected SiteContext Context;

		public EntityRepository(UnitOfWork unitOfWork)
		{
			this.Context = unitOfWork.Context;
			//_context.Roles.AddOrUpdate(role => role.Name,
			//			   new Role {Name = Role.Names.Administrator},
			//			   new Role {Name = Role.Names.Membership},
			//			   new Role {Name = Role.Names.Moderator});
			//_context.SaveChanges();
		}

		public EntityRepository()
		{
		}

		public void Create(TEntity entity)
		{
			Expect.ArgumentNotNull(entity);
			try
			{
				Context.Set<TEntity>().Add(entity);
				Context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		public TEntity Get(params object[] keyValues)
		{
			try
			{
				return Context.Set<TEntity>().Find(keyValues);
			}
			catch (Exception ex)
			{
				throw new DataException(DbErrorMessage, ex);
			}
		}

		public IEnumerable<TEntity> GetAll()
		{
			try
			{
				return Context.Set<TEntity>().ToList();
			}
			catch (Exception ex)
			{
				throw new DataException(DbErrorMessage, ex);
			}
		}

		public void Update(TEntity entity, params object[] keyValues)
		{
			Expect.ArgumentNotNull(entity);
			var contextEntry = Context.Entry(entity);
			if(contextEntry != null)
			{
				var oldEntity = Context.Set<TEntity>().Find(keyValues);
				if(oldEntity != null)
				{
					var contextOldEntry = Context.Entry(oldEntity);
					contextOldEntry.CurrentValues.SetValues(entity);
					contextOldEntry.State = EntityState.Modified;
					Context.SaveChanges();
				}
			}
		}

		public void Delete(params object[] keyValues)
		{
			try
			{
				var entity = Context.Set<TEntity>().Find(keyValues);
				if (entity != null)
				{
					Context.Set<TEntity>().Remove(entity);
					Context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new DataException(DbErrorMessage, ex);
			}
		}
	}
}
