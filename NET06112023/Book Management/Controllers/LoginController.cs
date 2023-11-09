using Book_Management.Models;
using Book_Management.ViewModel.Login;
using Book_Management.ViewModel.User;
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
        public IActionResult Registration(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                UserMst userMst = new UserMst();

                var userList = _db.UserMsts.Where(x => x.IsDelete == false && x.Email == registrationViewModel.Email).ToList();
                if (userList.Count <= 0)
                {
                    userMst.FullName = registrationViewModel.FullName.Trim();
                    userMst.Email = registrationViewModel.Email.Trim();
                    userMst.UserName = registrationViewModel.UserName.Trim();
                    userMst.Address = registrationViewModel.Address.Trim();
                    userMst.ContactNumber = registrationViewModel.ContactNumber.Trim();
                    userMst.Password = registrationViewModel.Password.Trim();
                    userMst.IsActive = true;
                    userMst.CreatedBy = 1;
                    userMst.CreatedOn = DateTime.Now;

                    _db.UserMsts.Add(userMst);
                    _db.SaveChanges();
                    return RedirectToAction("GetUser");
                }
                else
                {
                    ViewBag.Message = "Email is already Exists.";
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
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var user = _db.UserMsts.Where(x => x.UserName == loginViewModel.UserName && x.Password == loginViewModel.Password && x.IsDelete == false).FirstOrDefault();
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
