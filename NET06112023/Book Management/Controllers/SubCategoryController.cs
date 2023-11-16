using Book_Management.Models;
using Book_Management.ViewModel.Category;
using Book_Management.ViewModel.SubCategory;
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
            #region 
            //List<SubcategoryMst> subcategoryList = new List<SubcategoryMst>();
            //SubcategoryMst subcategoryMst = new SubcategoryMst();
            //var lstSubcategoryMst = _db.SubcategoryMsts.Where(u => u.IsDelete == false).ToList();

            //foreach (var item in lstSubcategoryMst)
            //{
            //    var categoryName = _db.CategoryMsts.FirstOrDefault(x => x.CategoryId == item.CategoryId && x.IsDelete == false);
            //    subcategoryMst = new SubcategoryMst();
            //    subcategoryMst.CategoryName = item.CategoryName;
            //    subcategoryMst.SubcategoryName = item.SubcategoryName;
            //    subcategoryMst.SubcategoryId = item.SubcategoryId;
            //    if (categoryName != null)
            //    {
            //        subcategoryMst.CategoryName = categoryName.CategoryName;
            //    }
            //    subcategoryList.Add(subcategoryMst);
            //}
            //ViewBag.subcategoryList = subcategoryList;
            //return View(subcategoryList); 
            #endregion

            var subCategoryList = (from u in _db.SubcategoryMsts.Where(u => u.IsDelete == false)
                                join j in _db.CategoryMsts.Where(u => u.IsDelete == false)
                                on u.CategoryId equals j.CategoryId
                                select new GetSubCategoryViewModel
                                {
                                    SubcategoryId = u.SubcategoryId,
                                    SubcategoryName = u.SubcategoryName,
                                    CategoryName = j.CategoryName
                                }).ToList();
            return View(subCategoryList);

        }

        [HttpGet]
        public IActionResult AddSubCategory()
        {
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");

            return View();
        }
        [HttpPost]
        public IActionResult AddSubCategory(AddSubCategoryViewModel addSubCategoryViewModel)
        {
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");

            if (ModelState.IsValid)
            {
                SubcategoryMst subcategoryMst = new SubcategoryMst();
                var subcategoryList = _db.SubcategoryMsts.Where(x => x.IsDelete == false && x.SubcategoryName == addSubCategoryViewModel.SubcategoryName.Trim()).ToList();
                if (subcategoryList.Count <= 0)
                {
                    subcategoryMst.CategoryId = addSubCategoryViewModel.CategoryId;
                    subcategoryMst.SubcategoryName = addSubCategoryViewModel.SubcategoryName.Trim();
                    subcategoryMst.IsActive = true;
                    subcategoryMst.CreatedBy = 1;
                    subcategoryMst.CreatedOn = DateTime.Now;

                    _db.SubcategoryMsts.Add(subcategoryMst);
                    _db.SaveChanges();
                    return RedirectToAction("GetSubCategory");
                }
                else
                {
                    ViewBag.Message = "subcategory name is already Exists.";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateSubCategory(int id)
        {
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");

            var updateSubCategory = _db.SubcategoryMsts.FirstOrDefault(x => x.SubcategoryId == id);
            if (updateSubCategory != null)
            {
                var updateSubCategoryView = new UpdateSubCategoryViewModel()
                {
                    SubcategoryId = updateSubCategory.SubcategoryId,
                    SubcategoryName = updateSubCategory.SubcategoryName,
                    CategoryId = updateSubCategory.CategoryId,
                };
            }
            return RedirectToAction("GetSubCategory");

        }
        [HttpPost]
        public IActionResult UpdateSubCategory(UpdateSubCategoryViewModel updateSubCategoryViewModel)
        {
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");

            var updateSubCategory = _db.SubcategoryMsts.FirstOrDefault(x => x.SubcategoryId == updateSubCategoryViewModel.SubcategoryId);
            if (updateSubCategory != null)
            {
                var subcategoryList = _db.SubcategoryMsts.Where(x => x.IsDelete == false && x.SubcategoryName == updateSubCategoryViewModel.SubcategoryName.Trim() && x.SubcategoryId != updateSubCategoryViewModel.SubcategoryId).ToList();
                if (subcategoryList.Count <= 0)
                {
                    updateSubCategory.CategoryId = updateSubCategoryViewModel.CategoryId;
                    updateSubCategory.SubcategoryName = updateSubCategoryViewModel.SubcategoryName.Trim();
                    updateSubCategory.UpdatedOn = DateTime.Now;
                    updateSubCategory.UpdateBy = 1;

                    _db.Entry(updateSubCategory).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("GetSubCategory");
                }
                else
                {
                    ViewBag.Message = "categoryname is already Exists.";
                }
            }
            return View();
        }

        public IActionResult DeleteSubCategory(int id)
        {
            var deleteSubCategory = _db.SubcategoryMsts.FirstOrDefault(x => x.SubcategoryId == id);
            if (deleteSubCategory != null)
            {
                deleteSubCategory.UpdatedOn = DateTime.Now;
                deleteSubCategory.UpdateBy = 1;
                deleteSubCategory.IsDelete = true;

                _db.Entry(deleteSubCategory).State = EntityState.Modified;
                _db.SaveChanges();
                TempData["DeleteSubCategory"] = "SubCategory successfully deleted!";
            }
            return RedirectToAction("GetSubCategory");
        }

        #region 
        //[NonAction]
        //private void CategoryList()
        //{
        //    var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
        //    ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");
        //} 
        #endregion
    }
}
