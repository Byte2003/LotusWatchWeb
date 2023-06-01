using LotusWatchData.Data;
using LotusWatchModel;
using LotusWatchModel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LotusWatchWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

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
                    Value = i.CategoryId.ToString(),
                }),
                BrandList = _db.Brands.ToList().Select(i => new SelectListItem
                {
                    Text = i.BrandName,
                    Value = i.BrandId.ToString(),
                }),
            };
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductVM obj, IFormFile file)
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



            TempData["sucesss"] = "Product has been created successfully";
            _db.Products.Add(obj.Product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {

            // Pass VM to view
            ProductVM productVM = new ProductVM
            {
                Product = _db.Products.Include(u => u.Brand).Include(u => u.Category).FirstOrDefault(u => u.ProductId == id),
                CategoryList = _db.Categories.ToList().Select(i => new SelectListItem
                {

                    Text = i.Name,
                    Value = i.CategoryId.ToString(),
                }),
                BrandList = _db.Brands.ToList().Select(i => new SelectListItem
                {
                    Text = i.BrandName,
                    Value = i.BrandId.ToString(),
                }),
            };
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM obj)
        {
            if (ModelState.IsValid)
            {

                _db.Products.Update(obj.Product);
                TempData["success"] = "Product has been updated successfully";
                _db.SaveChanges();
            }


            return RedirectToAction("Index");

        }
        #region API CALLS

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            IEnumerable<Product> products = _db.Products.Include(u => u.Category).Include(u => u.Brand).ToList();
            return Json(new { data = products });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var product = _db.Products.Include(u => u.Brand).Include(u => u.Category).FirstOrDefault(u => u.ProductId == id);

            if (product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }



            _db.Products.Remove(product);
            _db.SaveChanges();

            return Json(new { success = true, message = "Delete successfully" });
        }
        #endregion
    }
}
