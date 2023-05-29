using LotusWatchData.Data;
using LotusWatchData.Repository;
using LotusWatchData.Repository.IRepository;
using LotusWatchModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LotusWatch.Controllers
{
    public class CategoryController : Controller
	{
		//private readonly ApplicationDbContext _db;
		//public CategoryController(ApplicationDbContext db)
		//{
		//	_db = db;
		//}

		private readonly IUnitOfWork _unitOfWork;
		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			IEnumerable<Category> categories = _unitOfWork.Category.GetAll();
			return View(categories);
		}

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
			if(ModelState.IsValid)
			{
				_unitOfWork.Category.Add(obj);
				_unitOfWork.Save();
                TempData["success"] = "Category has been created successfully";
                return RedirectToAction("Index");
			}
			
            else return View(obj);
        }

		public IActionResult Edit(int id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var category = _unitOfWork.Category.GetFirstOrDefault( u=> u.Id == id);
			return category == null ? NotFound() : View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category has been updated successfully";

                return RedirectToAction("Index");
            }

            else return View(obj);
        }
        public IActionResult Details(int id)
		{
			var category = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
			return View(category);
		}
		
		public IActionResult Delete(int id)
		{
			if (id == null|| id ==0)
			{
				return NotFound();
			}
			var category = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
			if (category == null)
			{
				return NotFound() ;
			} else
			{
				return View(category);
			}
		}

		[HttpPost]
		public IActionResult Delete(Category obj)
		{
			if(obj == null)
			{
				return NotFound();
			}
			_unitOfWork.Category.Remove(obj);
			_unitOfWork.Save();
            TempData["success"] = "Category has been deleted successfully";
            return RedirectToAction("Index");
			
		
		}
		#region API 
		[HttpGet]
		public IActionResult GetAllCategories()
		{

			IEnumerable<Category> categories = _unitOfWork.Category.GetAll().ToList();
			return Json(new { data = categories });
		}
		#endregion
	}
}
