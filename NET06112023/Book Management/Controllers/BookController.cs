using Book_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Book_Management.Controllers
{
    public class BookController : Controller
    {
        private readonly BookManagementDbContext _db;
        public BookController(BookManagementDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetBook()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddBook(/*BookMst bookMst*/)
        {
            
            CategoryList();
            //List<SubcategoryMst> subcategorylist = new List<SubcategoryMst>();
            //subcategorylist = (from subcategory in _db.SubcategoryMsts where subcategory.CategoryId == bookMst.CategoryId select subcategory).ToList();
            //ViewBag.sublist = new SelectList(subcategorylist, "SubCategoryId", "SubcategoryName");
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(BookMst bookMst)
        {
            CategoryList();
            BookMst  Addbook = new BookMst();

            Addbook.CategoryId = bookMst.CategoryId;
            Addbook.SubcategoryId = bookMst.SubcategoryId;
            Addbook.BookName = bookMst.BookName;
            Addbook.AuthorName = bookMst.AuthorName;
            Addbook.BookPages = bookMst.BookPages;
            Addbook.Publisher = bookMst.Publisher;
            Addbook.PublishDate = bookMst.PublishDate;
            Addbook.Edition = bookMst.Edition;
            Addbook.Description = bookMst.Description;
            Addbook.Price = bookMst.Price;

            Addbook.IsActive = true;
            Addbook.CreatedBy = 1;
            Addbook.CreatedOn = DateTime.Now;


            return View();
        }
        

        [NonAction]
        private void CategoryList()
        {
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");
        }

        //[NonAction]
        //private void subCategoryList(BookMst book)
        //{
        //    List<SubcategoryMst> subcategorylist = new List<SubcategoryMst>();
        //    subcategorylist = (from subcategory in _db.SubcategoryMsts where subcategory.CategoryId == book.CategoryId select subcategory).ToList();
        //    ViewBag.sublist = new SelectList(subcategorylist, "SubcategoryId", "SubcategoryName");
        //}

       
        public JsonResult GetSubCategory(int CategoryId)
        {
            List<SubcategoryMst> subcategoryList = new List<SubcategoryMst>();
            subcategoryList = (from Subcategory in _db.SubcategoryMsts where Subcategory.CategoryId == CategoryId select Subcategory).ToList();
            subcategoryList.Insert(0, new SubcategoryMst { SubcategoryId = 0, SubcategoryName = "Select subcategory" });
            return Json(new SelectList(subcategoryList, "SubcategoryId", "SubcategoryName"));
        }
    }
}
