using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcKrisha.DAL;
using MvcKrisha.Models;

namespace MvcKrisha.Security
{
    public class UserManager
    {

        private KrishaContext db = new KrishaContext();

        public bool Login(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == username && u.Password == password);
            if (user != null)
            {
                SessionContainer.UserID = user.UserID.ToString();
                SessionContainer.Role = user.Role.ToString();
                return true;
            }
            return false;
        }

        public void LogOff()
        {
            SessionContainer.Remove("userid");
            SessionContainer.Remove("role");
        }

        public bool IsLogged
        {
            get { return !string.IsNullOrEmpty(SessionContainer.UserID); }
        }

        public bool Authorize(string allowedRoles)
        {
            if (!IsLogged) return false;
            if (!string.IsNullOrEmpty(allowedRoles.Trim()))
            {
                string[] roles = allowedRoles.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                if (roles.Count() > 0 && !roles.Contains(SessionContainer.Role)) return false;
            }
            return true;
        }

        public bool Register(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}