using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcKrisha.Models
{
    public class User
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string RePassword { get; set; }
        public int Role { get; set; }

        public virtual ICollection<Flat> Flats { get; set; } 

        public int UserID { get; set; }
       
    }

    public class LoginUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}