using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MvcKrisha.Models;
using MvcKrisha.DAL;
using MvcKrisha.Security;

namespace MvcKrisha.Controllers
{

    public class AccountController : Controller
    {
        private KrishaContext db = new KrishaContext();
        private UserManager mn = new UserManager();


        [CustomAuthorize(Roles = "0")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Login,Password,RePassword,Name,PhoneNumber,Email")] User user)
        {
                user.Role = 0;
                if (ModelState.IsValid)
                {
                    if (mn.Register(user)) return RedirectToAction("Index", "Users");
                    ViewBag.ErrorMessage = "That Login is already exist";
                }
     
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Login,Password")] LoginUser user)
        {
            if (ModelState.IsValid)
            {
                if (mn.Login(user.Login, user.Password))
                {
                    return RedirectToAction("Index", "Users");
                }
                ViewBag.ErrorMessage = "Login or Password does not match";
            }

            return View(user);
        }

        public ActionResult LogOff()
        {
            mn.LogOff();
            return RedirectToAction("Login");
        }
         

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}