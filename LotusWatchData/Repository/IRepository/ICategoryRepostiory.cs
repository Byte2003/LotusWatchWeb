using LotusWatchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusWatchData.Repository.IRepository
{
	public interface ICategoryRepostiory: IRepository<Category>
	{

		void Update(Category category);

		
	}
}
