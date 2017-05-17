using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcKrisha.Security
{
    public class SessionContainer
    {
        public static string UserID
        {
            get { return Getter("userid"); }
            set { Setter("userid", value); }
        }

        public static string Role
        {
            get { return Getter("role"); }
            set { Setter("role", value); }
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        private static string Getter(string key)
        {
            if (HttpContext.Current == null) return "";
            var sessionVar = HttpContext.Current.Session[key];
            return (sessionVar != null) ? sessionVar as string : "";
        }

        private static void Setter(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}