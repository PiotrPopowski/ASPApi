using PPRProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPRProject.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Role> Roles { get; }
        User CurrentUser { get; set; }
        void SaveUser(User user);
        void RemoveUser(int userID);
    }
}
