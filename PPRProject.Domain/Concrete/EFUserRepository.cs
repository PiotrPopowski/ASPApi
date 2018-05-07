using PPRProject.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPRProject.Domain.Entities;

namespace PPRProject.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }
        public IEnumerable<Role> Roles
        {
            get { return context.Roles; }
        }
        public User CurrentUser { get; set; }
        public void SaveUser(User user)
        {
            if (user.UserID == 0)
                context.Users.Add(user);
            else
            {
                User dbEntry = context.Users.Find(user.UserID);
                if (dbEntry != null)
                {
                    dbEntry.FirstName = user.FirstName;
                    dbEntry.LastName = user.LastName;
                    dbEntry.Login = user.Login;
                    dbEntry.Password = user.Password;
                    dbEntry.RoleID = user.RoleID;
                    dbEntry.Birth = user.Birth;
                    dbEntry.IsRemoved = user.IsRemoved;
                }
            }
            context.SaveChanges();
        }
        public void RemoveUser(int userID)
        {
             User user = context.Users.Find(userID);
             user.IsRemoved = 1;
             SaveUser(user);
        }

    }
}

