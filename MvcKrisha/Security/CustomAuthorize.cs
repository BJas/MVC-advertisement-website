using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKrisha.Security 
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!(new UserManager()).Authorize(this.Roles)) filterContext.Result = new RedirectResult("/Account/Login");
        }
    }
}