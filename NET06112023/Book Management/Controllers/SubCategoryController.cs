using Book_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Book_Management.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly BookManagementDbContext _db;
        public SubCategoryController(BookManagementDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetSubCategory()
        {
            List<SubcategoryMst> subcategoryList = new List<SubcategoryMst>();
            SubcategoryMst subcategoryMst = new SubcategoryMst();
            var lstSubcategoryMst = _db.SubcategoryMsts.Where(u => u.IsDelete == false).ToList();

            foreach (var item in lstSubcategoryMst)
            {
                var categoryName = _db.CategoryMsts.FirstOrDefault(x => x.CategoryId == item.CategoryId && x.IsDelete == false);
                subcategoryMst = new SubcategoryMst();
                subcategoryMst.CategoryName = item.CategoryName;
                subcategoryMst.SubcategoryName = item.SubcategoryName;
                subcategoryMst.SubcategoryId = item.SubcategoryId;
                if (categoryName != null)
                {
                    subcategoryMst.CategoryName = categoryName.CategoryName;
                }
                subcategoryList.Add(subcategoryMst);
            }
           // ViewBag.subcategoryList = subcategoryList;
            return View(subcategoryList);

        }

        [HttpGet]
        public IActionResult AddSubCategory()
        {
            CategoryList();

            return View();
        }
        [HttpPost]
        public IActionResult AddSubCategory(SubcategoryMst subcategory)
        {
            CategoryList();

            ModelState.Remove("CategoryId");
            ModelState.Remove("CategoryName");
            if (ModelState.IsValid)
            {
                SubcategoryMst subcategoryMst = new SubcategoryMst();
                var subcategoryList = _db.SubcategoryMsts.Where(x => x.IsDelete == false).ToList();
                if (subcategoryList.Where(u => u.SubcategoryName == subcategory.SubcategoryName).ToList().Count > 0)
                {
                    ViewBag.Message = "subcategory name is already Exists.";
                    return View();
                }
                else
                {
                    subcategoryMst.CategoryId = subcategory.CategoryId;
                    subcategoryMst.SubcategoryName =  subcategory.SubcategoryName;
                    subcategoryMst.IsActive = true;
                    subcategoryMst.CreatedBy = 1;
                    subcategoryMst.CreatedOn = DateTime.Now;

                    _db.SubcategoryMsts.Add(subcategoryMst);
                    _db.SaveChanges();

                    TempData["subcategoryAdded"] = "subcategory added SuccessFully!";
                    return RedirectToAction("GetSubCategory");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateSubCategory(int id)
        {
            CategoryList();

            var updateSubCategory = _db.SubcategoryMsts.FirstOrDefault(x => x.SubcategoryId == id);
            if (updateSubCategory != null)
            {
                return View(updateSubCategory);
            }
            return RedirectToAction("GetSubCategory");

        }
        [HttpPost]
        public IActionResult UpdateSubCategory(SubcategoryMst subcategory)
        {
            CategoryList();
            var updateSubCategory = _db.SubcategoryMsts.FirstOrDefault(x => x.SubcategoryId == subcategory.SubcategoryId);
            if (updateSubCategory != null)
            {
                var subcategoryList = _db.SubcategoryMsts.Where(x => x.IsDelete == false).ToList();
                if (subcategoryList.Where(u => u.SubcategoryName == subcategory.SubcategoryName).ToList().Count > 0)
                {
                    ViewBag.Message = "categoryname is already Exists.";
                    return View();
                }
                else
                {
                    updateSubCategory.CategoryId = subcategory.CategoryId;
                    updateSubCategory.SubcategoryName = subcategory.SubcategoryName;
                    updateSubCategory.UpdatedOn = DateTime.Now;
                    updateSubCategory.UpdateBy = subcategory.CategoryId;

                    _db.Entry(updateSubCategory).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("GetSubCategory");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteSubCategory(int id)
        {
            var deleteSubCategory = _db.SubcategoryMsts.FirstOrDefault(x => x.SubcategoryId == id);
            if (deleteSubCategory != null)
            {
                return View(deleteSubCategory);
            }
            return RedirectToAction("GetSubCategory");
        }

        [HttpPost]
        public IActionResult DeleteSubCategory(SubcategoryMst subcategory)
        {
            var deleteSubCategory = _db.SubcategoryMsts.FirstOrDefault(x => x.SubcategoryId == subcategory.SubcategoryId && x.IsDelete == false);

            if (deleteSubCategory != null)
            {
                deleteSubCategory.UpdatedOn = DateTime.Now;
                deleteSubCategory.UpdateBy = subcategory.CategoryId;
                deleteSubCategory.IsDelete = true;

                _db.Entry(deleteSubCategory).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("GetSubCategory");
            }
            return View();
        }

        [NonAction]
        private void CategoryList()
        {
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");
        }
    }
}
