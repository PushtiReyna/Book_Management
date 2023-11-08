using Book_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace Book_Management.Controllers
{
    public class LoginController : Controller
    {


        private readonly BookManagementDbContext _db;
        public LoginController(BookManagementDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(UserMst userMst)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                UserMst user = new UserMst();

                var userList = _db.UserMsts.Where(x => x.IsDelete == false).ToList();
                if (userList.Where(u => u.Email == userMst.Email).ToList().Count > 0)
                {
                    ViewBag.EmailMessage = "Email is already Exists.";
                    return View();
                }
                else if (userList.Where(u => u.UserName == userMst.UserName).ToList().Count > 0)
                {
                    ViewBag.UserNameMessage = "UserName is already Exists.";
                    return View();
                }
                else
                {
                    user.FullName = userMst.FullName.Trim();
                    user.Email = userMst.Email.Trim();
                    user.UserName = userMst.UserName.Trim();
                    user.Address = userMst.Address.Trim();
                    user.ContactNumber = userMst.ContactNumber.Trim();
                    user.Password = userMst.Password.Trim();
                    user.IsActive = true;
                    user.CreatedBy = 1;
                    user.CreatedOn = DateTime.Now;

                    _db.UserMsts.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("DashBoard");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserMst userMst)
        {
            var user = _db.UserMsts.Where(x => x.UserName == userMst.UserName && x.Password == userMst.Password && x.IsDelete == false).FirstOrDefault();

            if (user != null)
            {
               
                HttpContext.Session.SetString("UserSession", user.UserName);
                return RedirectToAction("DashBoard");
            }
            else
            {
                ViewBag.Message = "USERNAME OR PASSWORD NOT CORRECT";
                return View();
            }
        }

        public IActionResult DashBoard()
        {
            
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }

        public IActionResult LogOut()
        {
           
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
            }
            return RedirectToAction("Login");
        }

    }
}
