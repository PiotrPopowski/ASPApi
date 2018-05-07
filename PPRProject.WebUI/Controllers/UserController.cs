using PPRProject.Domain.Abstract;
using PPRProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPRProject.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository repository;
        public UserController(IUserRepository repo)
        {
            repository = repo;
        }
        // GET: User
        [Authorize(Roles="User")]
        public ActionResult Account()
        {
            return View(repository.CurrentUser);
        }
        [Authorize(Roles = "User")]
        public ActionResult Delete()
        {
            repository.RemoveUser(repository.CurrentUser.UserID);
            TempData["message"] = "User has been removed.";
            return RedirectToAction("Login","Account");
        }
        [Authorize(Roles = "User")]
        public ActionResult Edit()
        {
            return View(repository.CurrentUser);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (user.RoleID == 0)
                user.RoleID = 3;
            if (ModelState.IsValid)
            {
                repository.SaveUser(user);
                TempData["message"] = "Edited Succesfully";
                return RedirectToAction("Account");
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(user);
            }
        }
    }
}