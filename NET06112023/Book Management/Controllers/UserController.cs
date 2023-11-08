using Book_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Book_Management.Controllers
{
    public class UserController : Controller
    {
        private readonly BookManagementDbContext _db;
        public UserController(BookManagementDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            var userList = _db.UserMsts.Where(u => u.IsDelete == false).ToList();

            return View(userList);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddUser(UserMst userMst)
        {
            if (ModelState.IsValid)
            {
                UserMst user = new UserMst();

                var userList = _db.UserMsts.Where(x => x.IsDelete == false).ToList();
                if (userList.Where(u => u.Email == userMst.Email).ToList().Count > 0)
                {
                    ViewBag.Message = "Email is already Exists.";
                    return View();
                }
                else if (userList.Where(u => u.UserName == userMst.UserName).ToList().Count > 0)
                {
                    ViewBag.Message = "UserName is already Exists.";
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
                    return RedirectToAction("GetUser");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
           var updateUser = _db.UserMsts.FirstOrDefault(x => x.UserId == id);
            if (updateUser != null)
            {
                return View(updateUser);
            }
            return RedirectToAction("GetUser");
        }

        [HttpPost]
        public IActionResult UpdateUser(UserMst userMst)
        {
            var updateUser = _db.UserMsts.FirstOrDefault(x => x.UserId == userMst.UserId);
            if (updateUser != null)
            {
                var userList = _db.UserMsts.Where(x => x.IsDelete == false).ToList();
                if (userList.Where(u => u.Email == userMst.Email).ToList().Count > 0)
                {
                    ViewBag.Message = "Email is already Exists.";
                    return View();
                }
                else if (userList.Where(u => u.UserName == userMst.UserName).ToList().Count > 0)
                {
                    ViewBag.Message = "UserName is already Exists.";
                    return View();
                }
                else
                {
                    updateUser.FullName = userMst.FullName.Trim();
                    updateUser.Email = userMst.Email.Trim();
                    updateUser.UserName = userMst.UserName.Trim();
                    updateUser.Address = userMst.Address.Trim();
                    updateUser.ContactNumber = userMst.ContactNumber.Trim();
                    updateUser.Password = userMst.Password.Trim();
                    updateUser.UpdatedOn = DateTime.Now;
                    updateUser.UpdateBy = userMst.UserId;

                    _db.Entry(updateUser).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("GetUser");
                }
            }
            return View();
        }

        public IActionResult DeleteUser(int id)
        {
            var deleteUser = _db.UserMsts.FirstOrDefault(x => x.UserId == id && x.IsDelete == false);
            if (deleteUser != null)
            {
                deleteUser.UpdatedOn = DateTime.Now;
                deleteUser.UpdateBy = deleteUser.UserId;
                deleteUser.IsDelete = true;

                _db.Entry(deleteUser).State = EntityState.Modified;
                _db.SaveChanges();

            }
            return RedirectToAction("GetBook");
        }
    }
}
