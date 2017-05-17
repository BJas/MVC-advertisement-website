using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcKrisha.DAL;
using MvcKrisha.Models;
using MvcKrisha.Security;

namespace MvcKrisha.Controllers
{ 
    [CustomAuthorize(Roles = "0, 1")]
    public class UsersController : Controller
    {
        private KrishaContext db = new KrishaContext();

        // GET: Users
        public ActionResult Index()
        {
            int userId = int.Parse(SessionContainer.UserID);

            var flats = db.Flats.Include(a => a.Region).Where(a => a.UserID == userId);
            flats = flats.OrderByDescending(s => s.PublishedDate);
            ViewBag.Flat = flats;
            ViewBag.Profile = db.Users.Where(a => a.UserID == userId);
            return View(db.Flats.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        // GET: Flat/DetailsAdd/5
        public ActionResult DetailsAdd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = db.Flats.Find(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            db.SaveChanges();
            return View(flat);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Name,PhoneNumber,Email,Login,Password,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }
       
        // GET: Users/EditAdd/5
        public ActionResult EditAdd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = db.Flats.Find(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            RegionDropDownList(flat.RegionID);
            RoomDropDownList(flat.Room);
            TypeDropDownList(flat.Type);
            
            return View(flat);
        }
        private void RegionDropDownList(object selectedCity = null)
        {
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "City", selectedCity);
        }
        private void RoomDropDownList(object selectedRoom = null)
        {
            var RoomLst = new List<int> { 1, 2, 3, 4, 5 };
            ViewBag.Room = new SelectList(RoomLst,  selectedRoom);
        }

        private void TypeDropDownList(object selectedRoom = null)
        {
            var TypeLst = new List<string>();
            var TypeQry = from t in db.Flats
                          orderby t.Type
                          select t.Type;
            TypeLst.AddRange(TypeQry.Distinct());
            ViewBag.Type = new SelectList(TypeLst, selectedRoom);

        }
        // POST: Users/EditAdd/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdd([Bind(Include = "ID, Floor,FloorAll,Room,Price,Area,State,Type,BuildTime,Address,Views,Image,PhoneNumb,PublishedDate,Description,RegionID,UserID")] Flat flat, HttpPostedFileBase newfile, HttpPostedFileBase file)
        {
            if (newfile != null)
            {
                var dir_path = Server.MapPath("/Images/") + "\\";
                var site_path = "/Images/";
                var file_name = newfile.FileName;
                var path = dir_path + file_name;
                if (System.IO.File.Exists(path))
                {
                    file_name = String.Format("{0:dd-MM-yyyy-hh-mm-ss}_{1}", DateTime.Now, file_name);
                    path = dir_path + file_name;
                }
                newfile.SaveAs(path);
                flat.Image = site_path + file_name;
                flat.PublishedDate = DateTime.Now;
                int userId = int.Parse(SessionContainer.UserID);
                flat.UserID = userId;

                if (ModelState.IsValid)
                {
                    db.Entry(flat).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Users");
                }
            }
            else
            {

                ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "City", flat.RegionID);

                var RoomLst = new List<int> { 1, 2, 3, 4, 5 };
                ViewBag.room = new SelectList(RoomLst);

                var TypeLst = new List<string>();
                var TypeQry = from t in db.Flats
                              orderby t.Type
                              select t.Type;
                TypeLst.AddRange(TypeQry.Distinct());
                ViewBag.type = new SelectList(TypeLst);

                flat.PublishedDate = DateTime.Now;
                int userId = int.Parse(SessionContainer.UserID);
                flat.UserID = userId;

                if (ModelState.IsValid)
                {
                    db.Entry(flat).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Users");
                }
            }

            return View(flat);
        }
        // GET: Users/CreateAdd
        public ActionResult CreateAdd()
        {
            ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "City");

            var RoomLst = new List<int> { 1, 2, 3, 4, 5 };
            ViewBag.room = new SelectList(RoomLst);

            var TypeLst = new List<string>();
            var TypeQry = from t in db.Flats
                          orderby t.Type
                          select t.Type;
            TypeLst.AddRange(TypeQry.Distinct());
            ViewBag.type = new SelectList(TypeLst);

            return View();
        }

        // POST: Users/CreateAdd
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdd([Bind(Include = "Floor,FloorAll,Room,Price,Area,State,Type,BuildTime,Address,Description,PhoneNumb,Views,PublishedDate,RegionID")] Flat flat, HttpPostedFileBase file)
        {

            if (file != null)
            {
                var dir_path = Server.MapPath("/Images/") + "\\";
                var site_path = "/Images/";
                var file_name = file.FileName;
                var path = dir_path + file_name;
                if (System.IO.File.Exists(path))
                {
                    file_name = String.Format("{0:dd-MM-yyyy-hh-mm-ss}_{1}", DateTime.Now, file_name);
                    path = dir_path + file_name;
                }
                file.SaveAs(path);
                flat.Image = site_path + file_name;

                ViewBag.RegionID = new SelectList(db.Regions, "RegionID", "City", flat.RegionID);

                var RoomLst = new List<int> { 1, 2, 3, 4, 5 };
                ViewBag.room = new SelectList(RoomLst);

                var TypeLst = new List<string>();
                var TypeQry = from t in db.Flats
                              orderby t.Type
                              select t.Type;
                TypeLst.AddRange(TypeQry.Distinct());
                ViewBag.type = new SelectList(TypeLst);


                flat.Views = 0;
                flat.PublishedDate = DateTime.Now;
                
                    int userId = int.Parse(SessionContainer.UserID);
                    flat.UserID = userId;

                if (ModelState.IsValid)
                {
                    db.Flats.Add(flat);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(flat);
        }
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Name,PhoneNumber,Login,Email,Password,RePassword")] User user, HttpPostedFileBase newfileimg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Users");
            }
           return View(user);
        }

        // GET: Users/DeleteAdd/5
        public ActionResult DeleteAdd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flat flat = db.Flats.Find(id);
            if (flat == null)
            {
                return HttpNotFound();
            }
            return View(flat);
        }

        // POST: Users/DeleteAdd/5
        [HttpPost, ActionName("DeleteAdd")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flat flat = db.Flats.Find(id);
            db.Flats.Remove(flat);
            db.SaveChanges();
            return RedirectToAction("Index");
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
