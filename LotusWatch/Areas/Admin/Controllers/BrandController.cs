using LotusWatchData.Data;
using LotusWatchModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LotusWatchWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BrandController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Brands.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Brand has been created sucessfully";
                return RedirectToAction("Index");
            }
            return Ok();
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var brand = _db.Brands.FirstOrDefault(u => u.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }
            else
            {
                return View(brand);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            _db.Brands.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Brand has been updated sucessfully";

            return RedirectToAction("Index");


        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var brand = _db.Brands.FirstOrDefault(u => u.BrandId == id);
            return brand == null ? NotFound() : View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Brand obj)
        {
            _db.Brands.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Brand has been created sucessfully";
            return RedirectToAction("Index");
        }
        #region API
        [HttpGet]
        public IActionResult GetAllBrands()
        {
            IEnumerable<Brand> brands = _db.Brands.ToList();

            return Json(new { data = brands });
        }


        #endregion
    }
}
