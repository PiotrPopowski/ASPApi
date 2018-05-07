using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PPRProject.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public int IsRemoved { get; set; }

        public virtual Role Role { get; set; }
    }
}
