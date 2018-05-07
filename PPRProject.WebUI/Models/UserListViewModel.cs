using PPRProject.Domain.Abstract;
using PPRProject.Domain.Entities;
using PPRProject.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPRProject.WebUI.Models
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public User CurrentUser { get; set; }
    }
}