using PPRProject.Domain.Abstract;
using PPRProject.Domain.Entities;
using PPRProject.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SportsStore.WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IUserRepository repository;
        public AccountController(IUserRepository userRepository)
        {
            repository = userRepository;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User model)
        {
            model = repository.Users.FirstOrDefault(u => u.Login.Equals(model.Login) &&
                                     u.Password.Equals(model.Password));
            if(model!=null)
            {
                AuthorizationService auth = new AuthorizationService(repository);
                auth.Authorize(model);
                repository.CurrentUser = repository.Users.FirstOrDefault(u=>u.Login.Equals(model.Login)&&u.Password.Equals(model.Password));
                if(model.RoleID==1) return RedirectToAction("List", "Admin");
                if(model.RoleID==2) return RedirectToAction("List", "Moderator");
                if (model.RoleID==3) return RedirectToAction("Account", "User");
            }
            return View();
        }
        public ActionResult Create()
        {
            return View(new User());
        }
        [HttpPost]
        public ActionResult Create(User model)
        {
            model.RoleID = 3;
            if (ModelState.IsValid)
            {
                repository.SaveUser(model);
                TempData["message"] = "User has been created.";
                return Login(model);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            TempData["message"] = "Error";
            return RedirectToAction("Create");
        }
    }
}