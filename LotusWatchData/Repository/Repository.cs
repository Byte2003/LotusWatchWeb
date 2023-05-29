using LotusWatchData.Data;
using LotusWatchData.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LotusWatchData.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<TEntity> _dbSet;

		public Repository(ApplicationDbContext db)
		{
			_db = db;
			_dbSet = db.Set<TEntity>();
		}

		public void Add(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<TEntity> query = _dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			//  Apply the eager-loading expressions
			if (includeProperties != null)
			{
				foreach (var includeProperty in includeProperties.Split
											(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty);
				}
			}

			return query.ToList();
		}

		public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter, string? includeProperties = null, bool tracked = true)
		{
			IQueryable<TEntity> query;
			if (tracked)
			{
				query = _dbSet;
			}
			else
			{
				query = _dbSet.AsNoTracking();
			}
			query = query.Where(filter);


			if (includeProperties != null)
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return query.FirstOrDefault();

		}

		public void Remove(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entity)
		{
			_dbSet.RemoveRange(entity);
		}

		
	}
}
