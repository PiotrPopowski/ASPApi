using PPRProject.Domain.Abstract;
using PPRProject.Domain.Entities;
using PPRProject.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PPRProject.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IUserRepository repository;
        public int PageSize = 5;
        public AdminController(IUserRepository userRepository)
        {
            this.repository = userRepository;
        }
        [Authorize(Roles ="Admin")]
        public ViewResult List(int page = 1)
        {
            UserListViewModel model = new UserListViewModel()
            {
                Users = repository.Users
                      .OrderBy(u => u.UserID)
                      .Skip((page - 1) * PageSize)
                      .Take(PageSize),
                PagingInfo = new Models.PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Users.Count()
                }
            };
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int userID)
        {
            repository.RemoveUser(userID);
            TempData["message"] = "User has been removed.";
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View("Edit", new User());
        }
        [Authorize(Roles = "Admin")]
        public ViewResult Edit(int userID)
        {
            User user = repository.Users.FirstOrDefault(u => u.UserID == userID);
            return View(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                repository.SaveUser(user);
                TempData["message"] = string.Format("Saved {0} {1}", user.FirstName, user.LastName);
                return RedirectToAction("List");
            }
            else
            {
            IEnumerable<ModelError> info = ModelState.Values.SelectMany(v => v.Errors);
                return View(user);
            }
        }
    }
}