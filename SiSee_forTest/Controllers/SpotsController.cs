using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiSee_v1.Models;

namespace SiSee_v1.Controllers
{
    public class SpotsController : Controller
    {
        private sisdbEntities1 db = new sisdbEntities1();

        // GET: Spots
        public ActionResult Index()
        {
            var spot = db.Spot.Include(s => s.Area).Include(s => s.Class);
            return View(spot.ToList());
        }

        // GET: Spots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = db.Spot.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // GET: Spots/Create
        public ActionResult Create()
        {
            ViewBag.area_ID = new SelectList(db.Area, "area_ID", "area_Name");
            ViewBag.class_ID = new SelectList(db.Class, "class_ID", "class_Name");
            return View();
        }

        // POST: Spots/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "spot_ID,area_ID,spot_name,spot_tel,spot_context,spot_optimeS,spot_optimeE,spot_add,spot_fee,spot_score,spot_other,class_ID")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Spot.Add(spot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.area_ID = new SelectList(db.Area, "area_ID", "area_Name", spot.area_ID);
            ViewBag.class_ID = new SelectList(db.Class, "class_ID", "class_Name", spot.class_ID);
            return View(spot);
        }

        // GET: Spots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = db.Spot.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            ViewBag.area_ID = new SelectList(db.Area, "area_ID", "area_Name", spot.area_ID);
            ViewBag.class_ID = new SelectList(db.Class, "class_ID", "class_Name", spot.class_ID);
            return View(spot);
        }

        // POST: Spots/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "spot_ID,area_ID,spot_name,spot_tel,spot_context,spot_optimeS,spot_optimeE,spot_add,spot_fee,spot_score,spot_other,class_ID")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.area_ID = new SelectList(db.Area, "area_ID", "area_Name", spot.area_ID);
            ViewBag.class_ID = new SelectList(db.Class, "class_ID", "class_Name", spot.class_ID);
            return View(spot);
        }

        // GET: Spots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = db.Spot.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // POST: Spots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Spot spot = db.Spot.Find(id);
            db.Spot.Remove(spot);
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
