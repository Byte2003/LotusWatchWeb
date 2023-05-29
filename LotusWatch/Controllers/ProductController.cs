using LotusWatchData.Data;
using LotusWatchModel;
using LotusWatchModel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LotusWatch.Controllers
{
    public class ProductController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly IWebHostEnvironment _hostEnvironment;
		public ProductController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
		{
			_db = db;
			_hostEnvironment = hostEnvironment;
		}

		public IActionResult Index()
		{
			return View();
		}

		
		public IActionResult Create()
		{
			ProductVM productVM = new ProductVM
			{
				Product = new Product(),
				CategoryList = _db.Categories.ToList().Select(i => new SelectListItem
				{

					Text = i.Name,
					Value = i.Id.ToString(),
				}),
				BrandList = _db.Brands.ToList().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString(),
				}),
			};
			return View(productVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ProductVM obj,IFormFile file)
		{
			// Process file upload
			string wwwRootPath = _hostEnvironment.WebRootPath;
			if (file != null)
			{
				string fileName = Guid.NewGuid().ToString();
				var uploads = Path.Combine(wwwRootPath, @"image\products");
				var extension = Path.GetExtension(file.FileName);
				if (obj.Product.ImageUrl != null)
				{
					var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}
				}
				using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
				{
					file.CopyTo(fileStreams);
				}
				obj.Product.ImageUrl = @"\image\products\" + fileName + extension;
			}


			// Update in join table: BrandCategory 
			var cateId = obj.Product.CategoryId;
			var brandId = obj.Product.BrandId;

			var cate = _db.Categories.Include(u => u.Brands).ToList().FirstOrDefault(u => u.Id == cateId);
			cate.Brands.Add(_db.Brands.FirstOrDefault(u => u.Id == brandId));

			
			_db.Products.Add(obj.Product);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Edit(int id)
		{
			// Remove old record in join table : BrandCategory, then add new to update
			var product = _db.Products.Include(u => u.Brand).Include(u => u.Category).FirstOrDefault(u => u.Id == id);

			var cateId = product.CategoryId;
			var brandId = product.BrandId;

			var cate = _db.Categories.Include(u => u.Brands).ToList().FirstOrDefault(u => u.Id == cateId);
			cate.Brands.Remove(_db.Brands.FirstOrDefault(u => u.Id == brandId));

			_db.SaveChanges();

			// Pass VM to view
			ProductVM productVM = new ProductVM
			{
				Product = _db.Products.Include(u=> u.Brand).Include(u=> u.Category).FirstOrDefault(u => u.Id == id),
				CategoryList = _db.Categories.ToList().Select(i => new SelectListItem
				{

					Text = i.Name,
					Value = i.Id.ToString(),
				}),
				BrandList = _db.Brands.ToList().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString(),
				}),
			};
			return View(productVM);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ProductVM obj)
		{
			if (ModelState.IsValid) {

				_db.Products.Update(obj.Product);
				TempData["success"] = "Product has been updated successfully";
				_db.SaveChanges();
			}

			var cateId = obj.Product.CategoryId;
			var brandId = obj.Product.BrandId;


			//add new record after removed
			var cate = _db.Categories.Include(u => u.Brands).ToList().FirstOrDefault(u => u.Id == cateId);
			cate.Brands.Add(_db.Brands.FirstOrDefault(u => u.Id == brandId));
			_db.SaveChanges();
			return RedirectToAction("Index");
			
		}
		#region API CALLS

		[HttpGet]
		public IActionResult GetAllProducts()
		{
			IEnumerable<Product> products = _db.Products.Include(u => u.Category).Include(u => u.Brand).ToList();
			return Json( new {data = products});
		}

		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var product = _db.Products.Include( u => u.Brand).Include(u=> u.Category).FirstOrDefault(u => u.Id == id);	

			if (product == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}

			var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}

			// Update in join table: BrandCategory 
			var cateId = product.CategoryId;
			var brandId = product.BrandId;

			var cate = _db.Categories.Include(u => u.Brands).ToList().FirstOrDefault(u => u.Id == cateId);
			cate.Brands.Remove(_db.Brands.FirstOrDefault(u => u.Id == brandId));

			_db.Products.Remove(product);
			_db.SaveChanges();

			return Json(new { success = true, message = "Delete successfully"});
		}
        #endregion
    }
}
