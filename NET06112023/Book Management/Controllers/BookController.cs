using Book_Management.Models;
using Book_Management.ViewModel;
using Book_Management.ViewModel.Book;
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
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");

            return View();
        }
        [HttpPost]
        public IActionResult AddBook(AddBookViewModel addBookViewModel)
        {
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");


            if (ModelState.IsValid)
            {
                BookMst bookMst = new BookMst();


                var bookList = _db.BookMsts.Where(x => x.IsDelete == false && x.BookName == addBookViewModel.BookName).ToList();
                if (bookList.Count <= 0)
                {
                    string imgPath = UploadImage(addBookViewModel.Image);
                    string filePath = UploadFile(addBookViewModel.File);

                    bookMst.CategoryId = addBookViewModel.CategoryId;
                    bookMst.SubcategoryId = addBookViewModel.SubcategoryId;
                    bookMst.BookName = addBookViewModel.BookName.Trim();
                    bookMst.AuthorName = addBookViewModel.AuthorName.Trim();
                    bookMst.BookPages = addBookViewModel.BookPages;
                    bookMst.Publisher = addBookViewModel.Publisher.Trim();
                    bookMst.PublishDate = addBookViewModel.PublishDate;
                    bookMst.Edition = addBookViewModel.Edition.Trim();
                    bookMst.Description = addBookViewModel.Description.Trim();
                    bookMst.Price = addBookViewModel.Price.Trim();
                    bookMst.CoverImagePath = imgPath;
                    bookMst.PdfPath = filePath;
                    bookMst.IsActive = true;
                    bookMst.CreatedBy = 1;
                    bookMst.CreatedOn = DateTime.Now;

                    _db.BookMsts.Add(bookMst);
                    _db.SaveChanges();
                    return RedirectToAction("GetBook");

                }
                else
                {
                    ViewBag.Message = "book name is already Exists.";
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult UpdateBook(int id)
        {
            
            var updateBook = _db.BookMsts.FirstOrDefault(x => x.BookId == id);
            if (updateBook != null)
            {
                var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
                ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");

                List<SubcategoryMst> listsubcategory = new List<SubcategoryMst>();

                listsubcategory = (from subcategory in _db.SubcategoryMsts where subcategory.CategoryId == updateBook.CategoryId select subcategory).ToList();
                ViewBag.subcategoryName = new SelectList(listsubcategory, "SubcategoryId", "SubcategoryName");

                var updateBookView = new UpdateBookViewModel()
                {
                    CategoryId = updateBook.CategoryId,
                    SubcategoryId = updateBook.SubcategoryId,
                    BookName = updateBook.BookName,
                    AuthorName = updateBook.AuthorName,
                    BookPages = updateBook.BookPages,
                    Publisher = updateBook.Publisher,
                    PublishDate = updateBook.PublishDate,
                    Edition = updateBook.Edition,
                    Description = updateBook.Description,
                    Price = updateBook.Price
                };
                return View(updateBookView);

            }
            return RedirectToAction("GetBook");
        }

        [HttpPost]
        public IActionResult UpdateBook(UpdateBookViewModel updateBookViewModel)
        {
            var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
            ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");

            var updateBook = _db.BookMsts.FirstOrDefault(x => x.BookId == updateBookViewModel.BookId);

            if (updateBook != null)
            {
                List<SubcategoryMst> listsubcategory = new List<SubcategoryMst>();

                listsubcategory = (from subcategory in _db.SubcategoryMsts where subcategory.CategoryId == updateBook.CategoryId select subcategory).ToList();
                ViewBag.stateName = new SelectList(listsubcategory, "SubcategoryId", "SubcategoryName");


                var bookList = _db.BookMsts.Where(x => x.BookName == updateBookViewModel.BookName && x.BookId != updateBookViewModel.BookId).ToList();
                if (bookList.Count <= 0)
                {
                    if (updateBookViewModel.Image != null || updateBookViewModel.File != null)
                    {
                        string imgPath = UploadImage(updateBookViewModel.Image);
                        string filePath = UploadFile(updateBookViewModel.File);

                        updateBook.CoverImagePath = imgPath;
                        updateBook.PdfPath = filePath;
                    }

                    updateBook.CategoryId = updateBookViewModel.CategoryId;
                    updateBook.SubcategoryId = updateBookViewModel.SubcategoryId;
                    updateBook.BookName = updateBookViewModel.BookName.Trim();
                    updateBook.AuthorName = updateBookViewModel.AuthorName.Trim();
                    updateBook.BookPages = updateBookViewModel.BookPages;
                    updateBook.Publisher = updateBookViewModel.Publisher.Trim();
                    updateBook.PublishDate = updateBookViewModel.PublishDate;
                    updateBook.Edition = updateBookViewModel.Edition.Trim();
                    updateBook.Description = updateBookViewModel.Description.Trim();
                    updateBook.Price = updateBookViewModel.Price.Trim();
                    updateBook.UpdatedOn = DateTime.Now;
                    updateBook.UpdateBy = 1;

                    _db.Entry(updateBook).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("GetCategory");

                }
                else
                {
                    ViewBag.Message = "book name is already Exists.";
                }
            }
            return View();
        }

        public IActionResult DeleteBook(int id)
        {
            var deleteBook = _db.BookMsts.FirstOrDefault(x => x.BookId == id);
            if (deleteBook != null)
            {
                deleteBook.UpdatedOn = DateTime.Now;
                deleteBook.UpdateBy = 1;
                deleteBook.IsDelete = true;

                _db.Entry(deleteBook).State = EntityState.Modified;
                _db.SaveChanges();
            }
            return RedirectToAction("GetBook");
        }

        public IActionResult DownloadBook(int id)
        {
            var downoadBook = _db.BookMsts.FirstOrDefault(x => x.BookId == id);
            if (downoadBook != null)
            {
                string filePath = downoadBook.PdfPath;
                string fileName = downoadBook.BookName;

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
            return filePath;
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
            return filePath;
        }



        #region 
        //[NonAction]
        //private void CategoryList()
        //{
        //    var categoryList = _db.CategoryMsts.Where(u => u.IsDelete == false).ToList();
        //    ViewBag.categoryList = new SelectList(categoryList, "CategoryId", "CategoryName");
        //} 
        #endregion

        public JsonResult GetSubCategory(int CategoryId)
        {
            List<SubcategoryMst> subcategoryList = new List<SubcategoryMst>();
            subcategoryList = (from Subcategory in _db.SubcategoryMsts where Subcategory.CategoryId == CategoryId select Subcategory).ToList();
            subcategoryList.Insert(0, new SubcategoryMst { SubcategoryId = 0, SubcategoryName = "Select subcategory" });
            return Json(new SelectList(subcategoryList, "SubcategoryId", "SubcategoryName"));
        }
    }
}
