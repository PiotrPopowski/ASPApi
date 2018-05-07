using PPRProject.Domain.Abstract;
using PPRProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PPRProject.WebUI.Models
{
    public class AuthorizationService
    {
        IUserRepository repo;
        public AuthorizationService(IUserRepository rep)
        {
            repo = rep;
        }
        public void Authorize(User user)
        {
            var authTicket = new FormsAuthenticationTicket(
                                 1,                             // version
                                 user.Login,                      // user name
                                 DateTime.Now,                  // created
                                 DateTime.Now.AddMinutes(20),   // expires
                                 false,                    // persistent?
                                 repo.Roles.Single(r => r.RoleID == user.RoleID).RoleName                           // can be used to store roles
                                      );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }
    }
}