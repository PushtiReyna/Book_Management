using Book_Management.Models;
using Book_Management.ViewModel;
using Book_Management.ViewModel.Book;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;

namespace Book_Management.Controllers
{
    public class BookController : Controller
    {
        private readonly BookManagementDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public BookController(BookManagementDbContext db, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
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


                var bookList = _db.BookMsts.Where(x => x.IsDelete == false && x.BookName == addBookViewModel.BookName.Trim()).ToList();
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
                    BookId = updateBook.BookId,
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


                var bookList = _db.BookMsts.Where(x => x.BookName == updateBookViewModel.BookName.Trim() && x.BookId != updateBookViewModel.BookId).ToList();
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
                    return RedirectToAction("GetBook");

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
                TempData["DeleteBook"] = "Book successfully deleted!";
            }
            return RedirectToAction("GetBook");
        }

        //public IActionResult DownloadBook(int id)
        //{
        //    var downoadBook = _db.BookMsts.FirstOrDefault(x => x.BookId == id);
        //    byte fileByte = new byte();
        //    Byte[] fb = new byte[1000];
        //    string fileName = "";
        //    if (downoadBook != null)
        //    {
        //        string filePath = downoadBook.PdfPath;
        //        fileName = downoadBook.BookName;
        //        //string fileName1 = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + filePath);

        //        //string ServerDomain = _httpContextAccessor.HttpContext.Request.Host.Value;
        //        //var FileBaseURL = _configuration["FileBaseURL"].ToString();
        //        //var res = !string.IsNullOrWhiteSpace(ServerDomain) ? ServerDomain : !string.IsNullOrWhiteSpace(FileBaseURL) ? FileBaseURL : _webHostEnvironment.WebRootPath;
        //        //filePath = Path.Combine(res, fileName);




        //        if (System.IO.File.Exists(fileName))
        //        {
        //            fileByte = Convert.ToByte(filePath);
        //        }

        //        string directoryPath = filePath != null  ? "/PDF" : "";
        //        string relativeRootPath = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + directoryPath;

        //        filePath = Path.Combine(relativeRootPath, fileName);
        //        FileStream fileStream = System.IO.File.OpenRead(filePath);
        //        fb = new byte[fileByte];
        //        fb = new Byte[fb.Length];

        //        return File(fb, "application/force-download", fileName);
        //    }
        //    //return Redirect("GetBook");
        //    return File(fb, "application/force-download", fileName);
        //}

      
        public IActionResult DownloadBook(int id)
        {
            //var downoadBook = _db.BookMsts.FirstOrDefault(x => x.BookId == id);
            //string fileName = "";
            //string filePath = "";

            //byte[] fileBytes = new byte[1024];
            //if (downoadBook != null)
            //{
            //     filePath = downoadBook.PdfPath;
            //    fileName = downoadBook.BookName;
            //    filePath = Path.Combine(_webHostEnvironment.WebRootPath,filePath);
            //    if (System.IO.File.Exists(filePath))
            //    {
            //        //WebClient webClient = new WebClient();
            //        // fileBytes = webClient.DownloadData(filePath);
            //        var fileInfo = new System.IO.FileInfo(filePath);
            //        Response.ContentType = "application/pdf";
            //        Response.Headers.Add("Content-Disposition", "attachment;filename=\"" + fileInfo.Name + "\"");
            //        Response.Headers.Add("Content-Length", fileInfo.Length.ToString());

            //        // Send the file to the client


            //    }
            //}
            //return File(System.IO.File.ReadAllBytes(filePath), "application/pdf", fileName);

            var downoadBook = _db.BookMsts.FirstOrDefault(x => x.BookId == id);

            string fileName = downoadBook.BookName;
            string filePath = downoadBook.PdfPath;
            var path = Path.Combine(_webHostEnvironment.WebRootPath , filePath);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            // memory.Position = 0;

            //return File(memory, GetContentType(path), Path.GetFileName(path));
            byte[] data = memory.ToArray();

           // response.Data = (data, Path.GetFileName(path));

            return File(data, "application/pdf", fileName);

        }
        private string UploadImage(IFormFile Image)
        {
            string fileName = Image.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

            string filePath = Path.Combine("Images", fileName);
            if (fileName != null)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            return filePath;
        }
        private string UploadFile(IFormFile File)
        {
            string fileName = File.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "PDF", fileName);

            string filePath = Path.Combine("PDF", fileName);
            if (fileName != null)
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
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
