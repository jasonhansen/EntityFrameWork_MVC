using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApplication.Data;
using MyWebApplication.Models;

namespace MyWebApplication.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // list all the category/ using Dependency Injection
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }
        // 1: Create a category
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] // handler the function
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot be the same exactly with the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // 2: Edit a category (update)
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) // check if it is not a valid id
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
             if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost] // handler the function
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // 3: Delete a category
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) // check if it is not a valid id
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")] // handler the function
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
