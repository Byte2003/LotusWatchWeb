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
	public class CategoryRepository : Repository<Category>, ICategoryRepostiory
	{
		private ApplicationDbContext _db;

		public CategoryRepository(ApplicationDbContext db)  : base(db)
		{
			_db = db;
		}

		public void Update(Category category)
		{
			_db.Categories.Update(category);
		}

		
	}
}
