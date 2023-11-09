using Book_Management.Models;
using Book_Management.ViewModel.Book;
using Book_Management.ViewModel.Category;
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
        public IActionResult AddCategory(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                CategoryMst categoryMst = new CategoryMst();
                var categoryList = _db.CategoryMsts.Where(x => x.IsDelete == false && x.CategoryName == addCategoryViewModel.CategoryName).ToList();
                if (categoryList.Count <= 0)
                {
                    categoryMst.CategoryName = addCategoryViewModel.CategoryName.Trim();
                    categoryMst.IsActive = true;
                    categoryMst.CreatedBy = 1;
                    categoryMst.CreatedOn = DateTime.Now;

                    _db.CategoryMsts.Add(categoryMst);
                    _db.SaveChanges();

                    return RedirectToAction("GetCategory");
                }
                else
                {
                    ViewBag.Message = "categoryname is already Exists.";
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
                var updateCategoryView = new UpdateCategoryViewModel()
                {
                    CategoryName = updateCategory.CategoryName,
                };
                return View(updateCategoryView);
            }
            return RedirectToAction("GetCategory");

        }
        [HttpPost]
        public IActionResult UpdateCategory(UpdateCategoryViewModel updateCategoryViewModel)
        {
            var updateCategory = _db.CategoryMsts.FirstOrDefault(x => x.CategoryId == updateCategoryViewModel.CategoryId);
            if (updateCategory != null)
            {
                var categoryList = _db.CategoryMsts.Where(x => x.IsDelete == false && x.CategoryName == updateCategoryViewModel.CategoryName && x.CategoryId != updateCategoryViewModel.CategoryId).ToList();
                if (categoryList.Count <= 0)
                {
                    updateCategory.CategoryName = updateCategoryViewModel.CategoryName.Trim();
                    updateCategory.UpdatedOn = DateTime.Now;
                    updateCategory.UpdateBy = 1;

                    _db.Entry(updateCategory).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("GetCategory");
                }
                else
                {
                    ViewBag.Message = "categoryname is already Exists.";
                }
            }
            return View();
        }

        public IActionResult DeleteCategory(int id)
        { 
            var deleteCategory = _db.CategoryMsts.FirstOrDefault(x => x.CategoryId == id);
            if (deleteCategory != null)
            {
                deleteCategory.UpdatedOn = DateTime.Now;
                deleteCategory.UpdateBy = 1;
                deleteCategory.IsDelete = true;
                _db.SubcategoryMsts.RemoveRange();
                _db.Entry(deleteCategory).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("GetCategory");
        }
    }

}

