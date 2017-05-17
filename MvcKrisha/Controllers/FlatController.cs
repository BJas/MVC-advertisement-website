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
using PagedList;
using MvcKrisha.Security;

namespace MvcKrisha.Controllers
{
    
    public class FlatController : Controller
    {
        private KrishaContext db = new KrishaContext();

        // GET: Flat
        public ActionResult Index(string sortOrder, string floorFrom, string floorTo, string areaFrom, string areaTo, string builtFrom, string builtTo,
                                    string priceFrom, string priceTo, string room, string type, string region, string currentFilter, string searchString, int? page)
        {
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "PublishedDate" ? "" : "PublishedDate";
            ViewBag.ViewSortParm = sortOrder == "Views" ? "view_desc" : "Views";

            var flats = db.Flats.Include(s => s.Region).Include(s => s.User);

            switch (sortOrder)
            {

                case "Price":
                    flats = flats.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    flats = flats.OrderByDescending(s => s.Price);
                    break;
                case "Views":
                    flats = flats.OrderBy(s => s.Views);
                    break;
                case "view_desc":
                    flats = flats.OrderByDescending(s => s.Views);
                    break;
                case "PublishedDate":
                    flats = flats.OrderBy(s => s.PublishedDate);
                    break;
                default:
                    flats = flats.OrderByDescending(s => s.PublishedDate);
                    break;
            }

            var RegionLst = new List<string> { "Almaty", "Astana", "Shymkent", "Taraz", "Atyrau", "Aktobe", "Aktau",
                                               "Kyzylorda", "Karaganda", "Pavlodar", "Semey", "Taldykorgan",
                                               "Uralsk", "Kostanai", "Petropavlovsk" };

            ViewBag.region = new SelectList(RegionLst);

            var RoomLst = new List<int>();

            var RoomQry = from d in db.Flats
                          orderby d.Room
                          select d.Room;

            RoomLst.AddRange(RoomQry.Distinct());
            ViewBag.room = new SelectList(RoomLst);

            var TypeLst = new List<string>();
            var TypeQry = from t in db.Flats
                          orderby t.Type
                          select t.Type;
            TypeLst.AddRange(TypeQry.Distinct());
            ViewBag.type = new SelectList(TypeLst);

            if (!String.IsNullOrEmpty(searchString))
            {
                flats = flats.Where(s => s.Address.Contains(searchString)
                                        || s.Description.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(type))
            {
                flats = flats.Where(x => x.Type == type);
            }

            if (!String.IsNullOrEmpty(region))
            {
                flats = flats.Where(x => x.Region.City == region);
            }

            if (!String.IsNullOrEmpty(room))
            {
                flats = flats.Where(x => x.Room.ToString() == room);
            }

            if (!String.IsNullOrEmpty(priceFrom) && !String.IsNullOrEmpty(priceTo))
            {
                int p1 = int.Parse(priceFrom);
                int p2 = int.Parse(priceTo);

                flats = flats.Where(x => x.Price >= p1 && x.Price <= p2);
            }

            else if (String.IsNullOrEmpty(priceFrom) && !String.IsNullOrEmpty(priceTo))
            {
                int p2 = int.Parse(priceTo);

                flats = flats.Where(x => x.Price <= p2);
            }

            else if (!String.IsNullOrEmpty(priceFrom) && String.IsNullOrEmpty(priceTo))
            {
                int p1 = int.Parse(priceFrom);

                flats = flats.Where(x => x.Price >= p1);
            }

            if (!String.IsNullOrEmpty(floorFrom) && !String.IsNullOrEmpty(floorTo))
            {
                int f1 = int.Parse(floorFrom);
                int f2 = int.Parse(floorTo);

                flats = flats.Where(x => x.Floor >= f1 && x.Floor <= f2);
            }

            else if (!String.IsNullOrEmpty(floorFrom) && String.IsNullOrEmpty(floorTo))
            {
                int f1 = int.Parse(floorFrom);

                flats = flats.Where(x => x.Floor >= f1);
            }

            else if (String.IsNullOrEmpty(floorFrom) && !String.IsNullOrEmpty(floorTo))
            {
                int f2 = int.Parse(floorTo);

                flats = flats.Where(x => x.Floor <= f2);
            }

            if (!String.IsNullOrEmpty(areaFrom) && !String.IsNullOrEmpty(areaTo))
            {
                int a1 = int.Parse(areaFrom);
                int a2 = int.Parse(areaTo);

                flats = flats.Where(x => x.Area >= a1 && x.Area <= a2);
            }

            else if (!String.IsNullOrEmpty(areaFrom) && String.IsNullOrEmpty(areaTo))
            {
                int a1 = int.Parse(areaFrom);

                flats = flats.Where(x => x.Area >= a1);
            }

            else if (String.IsNullOrEmpty(areaFrom) && !String.IsNullOrEmpty(areaTo))
            {
                int a2 = int.Parse(areaTo);

                flats = flats.Where(x => x.Area <= a2);
            }

            if (!String.IsNullOrEmpty(builtFrom) && !String.IsNullOrEmpty(builtTo))
            {
                int a1 = int.Parse(builtFrom);
                int a2 = int.Parse(builtTo);

                flats = flats.Where(x => x.BuildTime >= a1 && x.BuildTime <= a2);
            }

            else if (!String.IsNullOrEmpty(builtFrom) && String.IsNullOrEmpty(builtTo))
            {
                int a1 = int.Parse(builtFrom);

                flats = flats.Where(x => x.BuildTime >= a1);
            }

            else if (String.IsNullOrEmpty(builtFrom) && !String.IsNullOrEmpty(builtTo))
            {
                int a2 = int.Parse(builtTo);

                flats = flats.Where(x => x.BuildTime <= a2);
            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(flats.ToPagedList(pageNumber, pageSize));
        }
        // GET: Flat/Details/5
        public ActionResult Details(int? id)
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
            flat.Views++;
            db.SaveChanges();
            return View(flat);
        }

        // GET: Flat/Create
        public ActionResult Create()
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

        // POST: Flat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Floor,FloorAll,Room,Price,Area,State,Type,BuildTime,Address,Description,PhoneNumb,Views,PublishedDate,RegionID")] Flat flat, HttpPostedFileBase file)
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
                if (SessionContainer.UserID != null && SessionContainer.Role == "0")
                {
                    int userId = int.Parse(SessionContainer.UserID);
                    flat.UserID = userId;
                }
                else
                {
                    flat.UserID = 1;
                }
                if (ModelState.IsValid)
                {
                    db.Flats.Add(flat);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
                return View(flat);
        }

        // GET: Flat/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Flat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Floor,FloorAll,Room,Price,Area,State,Type,BuildTime,Address,Description,Views,PublishedDate,Image")] Flat flat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flat);
        }

        // GET: Flat/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Flat/Delete/5
        [HttpPost, ActionName("Delete")]
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
