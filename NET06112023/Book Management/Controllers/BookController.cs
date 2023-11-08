using Book_Management.Models;
using Book_Management.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Book_Management.Controllers
{
    public class BookController : Controller
    {
        private readonly BookManagementDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(BookManagementDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetBook()
        {
            var bookList = _db.BookMsts.Where(u => u.IsDelete == false).ToList();

            return View(bookList);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            CategoryList();
            return View();
        }
        [HttpPost]
        public IActionResult AddBook(BookViewModel bookMst)
        {
            CategoryList();

            ModelState.Remove("BookId");
            ModelState.Remove("CategoryId");
            ModelState.Remove("SubcategoryId");

            if (ModelState.IsValid)
            {
                BookMst addbook = new BookMst();


                var bookList = _db.BookMsts.Where(x => x.IsDelete == false).ToList();
                if (bookList.Where(u => u.BookName == bookMst.BookName).ToList().Count > 0)
                {
                    ViewBag.Message = "book name is already Exists.";
                    return View();
                }
                else
                {
                    string imgPath = UploadImage(bookMst.Image);
                    string filePath = UploadFile(bookMst.File);

                    addbook.CategoryId = bookMst.CategoryId;
                    addbook.SubcategoryId = bookMst.SubcategoryId;
                    addbook.BookName = bookMst.BookName.Trim();
                    addbook.AuthorName = bookMst.AuthorName.Trim();
                    addbook.BookPages = bookMst.BookPages;
                    addbook.Publisher = bookMst.Publisher.Trim();
                    addbook.PublishDate = bookMst.PublishDate;
                    addbook.Edition = bookMst.Edition.Trim();
                    addbook.Description = bookMst.Description.Trim();
                    addbook.Price = bookMst.Price.Trim();
                    addbook.CoverImagePath = imgPath;
                    addbook.PdfPath = filePath;
                    addbook.IsActive = true;
                    addbook.CreatedBy = 1;
                    addbook.CreatedOn = DateTime.Now;

                    _db.BookMsts.Add(addbook);
                    _db.SaveChanges();
                    return RedirectToAction("GetBook");
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult UpdateBook(int id)
        {
            CategoryList();

            var updateBook = _db.BookMsts.FirstOrDefault(x => x.BookId == id);
            if (updateBook != null)
            {
                // var subcategorylist = _db.SubcategoryMsts.FirstOrDefault(x => x.IsDelete == false);
                List<SubcategoryMst> listsubcategory = new List<SubcategoryMst>();

                listsubcategory = (from subcategory in _db.SubcategoryMsts where subcategory.CategoryId == updateBook.CategoryId select subcategory).ToList();
                ViewBag.stateName = new SelectList(listsubcategory, "SubcategoryId", "SubcategoryName");

                return View(updateBook);
            }
            return RedirectToAction("GetBook");
        }

        [HttpPost]
        public IActionResult UpdateBook(BookMst bookMst)
        {
            CategoryList();
            var updateBook = _db.BookMsts.FirstOrDefault(x => x.BookId == bookMst.BookId);
            if (updateBook != null)
            {
                List<SubcategoryMst> listsubcategory = new List<SubcategoryMst>();

                listsubcategory = (from subcategory in _db.SubcategoryMsts where subcategory.CategoryId == updateBook.CategoryId select subcategory).ToList();
                ViewBag.stateName = new SelectList(listsubcategory, "SubcategoryId", "SubcategoryName");

                var bookList = _db.BookMsts.Where(x => x.IsDelete == false).ToList();
                if (bookList.Where(u => u.BookName == bookMst.BookName && u.BookId != bookMst.BookId).ToList().Count > 0)
                {
                    ViewBag.Message = "book name is already Exists.";
                    return View();
                }
                else
                {
                    if (bookMst.Image != null || bookMst.File != null)
                    {
                        string imgPath = UploadImage(bookMst.Image);
                        string filePath = UploadFile(bookMst.File);

                        updateBook.CoverImagePath = imgPath;
                        updateBook.PdfPath = filePath;
                    }

                    updateBook.CategoryId = bookMst.CategoryId;
                    updateBook.SubcategoryId = bookMst.SubcategoryId;
                    updateBook.BookName = bookMst.BookName.Trim();
                    updateBook.AuthorName = bookMst.AuthorName.Trim();
                    updateBook.BookPages = bookMst.BookPages;
                    updateBook.Publisher = bookMst.Publisher.Trim();
                    updateBook.PublishDate = bookMst.PublishDate;
                    updateBook.Edition = bookMst.Edition.Trim();
                    updateBook.Description = bookMst.Description.Trim();
                    updateBook.Price = bookMst.Price.Trim();
                    updateBook.UpdatedOn = DateTime.Now;
                    updateBook.UpdateBy = bookMst.BookId;

                    _db.Entry(updateBook).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("GetCategory");
                }
            }
            return View();
        }

        public IActionResult DeleteBook(int id)
        {
            var deleteBook = _db.BookMsts.FirstOrDefault(x => x.BookId == id && x.IsDelete == false);
            if (deleteBook != null)
            {
                deleteBook.UpdatedOn = DateTime.Now;
                deleteBook.UpdateBy = deleteBook.BookId;
                deleteBook.IsDelete = true;

                _db.Entry(deleteBook).State = EntityState.Modified;
                _db.SaveChanges();
                
            }
            return RedirectToAction("GetBook");
        }

        public IActionResult DownloadBook(int id)
        {
            var deleteBook = _db.BookMsts.FirstOrDefault(x => x.BookId == id && x.IsDelete == false);
            if (deleteBook != null)
            {
                string filePath = deleteBook.PdfPath;
                string fileName = deleteBook.BookName;

                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                return File(fileBytes, "application/force-download", fileName);
            }
            return RedirectToAction("GetBook");
        }

        private string UploadImage(IFormFile Image)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            string fileName = Image.FileName;
            string filePath = Path.Combine(path, fileName);
            if (fileName != null)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            //return filePath;
            return fileName;
        }
        private string UploadFile(IFormFile File)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "PDF");
            string fileName = File.FileName;
            string filePath = Path.Combine(path, fileName);
            if (fileName != null)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    File.CopyTo(fileStream);
                }
            }
            //return filePath;
            return fileName;
        }



        [NonAction]
        private void CategoryList()
        {
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");
        }

        public JsonResult GetSubCategory(int CategoryId)
        {
            List<SubcategoryMst> subcategoryList = new List<SubcategoryMst>();
            subcategoryList = (from Subcategory in _db.SubcategoryMsts where Subcategory.CategoryId == CategoryId select Subcategory).ToList();
            subcategoryList.Insert(0, new SubcategoryMst { SubcategoryId = 0, SubcategoryName = "Select subcategory" });
            return Json(new SelectList(subcategoryList, "SubcategoryId", "SubcategoryName"));
        }
    }
}
