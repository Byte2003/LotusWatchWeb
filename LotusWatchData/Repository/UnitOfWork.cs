using LotusWatchData.Data;
using LotusWatchData.Repository.IRepository;
using LotusWatchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusWatchData.Repository
{
	public class UnitOfWork: IUnitOfWork
	{
		private ApplicationDbContext _db;

		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			Category = new CategoryRepository(_db);

		}

		public ICategoryRepostiory Category { get; private set; }

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
