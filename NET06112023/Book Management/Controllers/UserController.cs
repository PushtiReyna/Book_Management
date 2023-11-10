using Book_Management.Models;
using Book_Management.ViewModel.User;
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
        public IActionResult AddUser(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                UserMst userMst = new UserMst();

                var userList = _db.UserMsts.Where(x => x.IsDelete == false && x.Email == addUserViewModel.Email).ToList();
                if (userList.Count <= 0)
                {
                    userMst.FullName = addUserViewModel.FullName.Trim();
                    userMst.Email = addUserViewModel.Email.Trim();
                    userMst.UserName = addUserViewModel.UserName.Trim();
                    userMst.Address = addUserViewModel.Address.Trim();
                    userMst.ContactNumber = addUserViewModel.ContactNumber.Trim();
                    userMst.Password = addUserViewModel.Password.Trim();
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
        public IActionResult UpdateUser(int id)
        {
            var updateUser = _db.UserMsts.FirstOrDefault(x => x.UserId == id);
            if (updateUser != null)
            {
                var updateUserView = new UpdateUserViewModel()
                {
                    UserId = updateUser.UserId,
                    FullName = updateUser.FullName,
                    Email = updateUser.Email,
                    UserName = updateUser.UserName,
                    Address = updateUser.Address,
                    ContactNumber = updateUser.ContactNumber,
                };

                return View(updateUserView);
            }
            return RedirectToAction("GetUser");
        }

        [HttpPost]
        public IActionResult UpdateUser(UpdateUserViewModel updateUserViewModel)
        {
            var updateUser = _db.UserMsts.FirstOrDefault(x => x.UserId == updateUserViewModel.UserId);
            if (updateUser != null)
            {
                var userList = _db.UserMsts.Where(x => x.IsDelete == false && x.Email == updateUserViewModel.Email && x.UserId != updateUserViewModel.UserId).ToList();
                if (userList.Count <= 0)
                {
                    updateUser.FullName = updateUserViewModel.FullName.Trim();
                    updateUser.Email = updateUserViewModel.Email.Trim();
                    updateUser.UserName = updateUserViewModel.UserName.Trim();
                    updateUser.Address = updateUserViewModel.Address.Trim();
                    updateUser.ContactNumber = updateUserViewModel.ContactNumber.Trim();
                    updateUser.UpdatedOn = DateTime.Now;
                    updateUser.UpdateBy = 1;

                    _db.Entry(updateUser).State = EntityState.Modified;
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

        public IActionResult DeleteUser(int id)
        {
            var deleteUser = _db.UserMsts.FirstOrDefault(x => x.UserId == id);
            if (deleteUser != null)
            {
                deleteUser.UpdatedOn = DateTime.Now;
                deleteUser.UpdateBy = 1;
                deleteUser.IsDelete = true;

                _db.Entry(deleteUser).State = EntityState.Modified;
                _db.SaveChanges();

            }
            return RedirectToAction("GetUser");
        }
    }
}
