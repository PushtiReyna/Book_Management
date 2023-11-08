using Book_Management.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace Book_Management.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookManagementDbContext _db;
        public CategoryController(BookManagementDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetCategory()
        {
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();

            return View(categoryList);
        }
        //[HttpGet]
        //public string Get()
        //{
        //    var categoryList = _db.CategoryMsts.ToList();
        //    var result = JsonConvert.SerializeObject(new { data = categoryList });
        //    return (result);
        //}

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryMst category)
        {

            ModelState.Remove("CategoryId");

            if (ModelState.IsValid)
            {
                CategoryMst categoryMst = new CategoryMst();
                var categoryList = _db.CategoryMsts.Where(x => x.IsDelete == false).ToList();
                if (categoryList.Where(u => u.CategoryName == category.CategoryName).ToList().Count > 0)
                {
                    ViewBag.Message = "categoryname is already Exists.";
                    return View();
                }
                else
                {
                    categoryMst.CategoryName = category.CategoryName.Trim();
                    categoryMst.IsActive = true;
                    categoryMst.CreatedBy = 1;
                    categoryMst.CreatedOn = DateTime.Now;

                    _db.CategoryMsts.Add(categoryMst);
                    _db.SaveChanges();

                    return RedirectToAction("GetCategory");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var updateCategory = _db.CategoryMsts.FirstOrDefault(x => x.CategoryId == id);
            if (updateCategory != null)
            {
                return View(updateCategory);
            }
            return RedirectToAction("GetCategory");

        }
        [HttpPost]
        public IActionResult UpdateCategory(CategoryMst category)
        {
            var updateCategory = _db.CategoryMsts.FirstOrDefault(x => x.CategoryId == category.CategoryId);
            if (updateCategory != null)
            {
                var categoryList = _db.CategoryMsts.Where(x => x.IsDelete == false).ToList();
                if (categoryList.Where(u => u.CategoryName == category.CategoryName && u.CategoryId != category.CategoryId).ToList().Count > 0)
                {
                    ViewBag.Message = "categoryname is already Exists.";
                    return View();
                }
                else
                {
                    updateCategory.CategoryName = category.CategoryName.Trim();
                    updateCategory.UpdatedOn = DateTime.Now;
                    updateCategory.UpdateBy = category.CategoryId;

                    _db.Entry(updateCategory).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("GetCategory");
                }
            }
            return View();
        }

        public IActionResult DeleteCategory(int id)
        { 
            var deleteCategory = _db.CategoryMsts.FirstOrDefault(x => x.CategoryId == id && x.IsDelete == false);
            if (deleteCategory != null)
            {
                deleteCategory.UpdatedOn = DateTime.Now;
                deleteCategory.UpdateBy = deleteCategory.CategoryId;
                deleteCategory.IsDelete = true;
                _db.Entry(deleteCategory).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("GetCategory");
        }
    }

}

