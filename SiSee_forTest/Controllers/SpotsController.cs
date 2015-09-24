using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            var spot = db.Spot.Include(s => s.Area);
            return View(await spot.ToListAsync());
        }

        // GET: Spots/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = await db.Spot.FindAsync(id);
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
            return View();
        }

        // POST: Spots/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "spot_ID,area_ID,spot_name,spot_tel,spot_context,spot_optimeS,spot_add,spot_fee,spot_score,spot_other,spot_loaction")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Spot.Add(spot);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.area_ID = new SelectList(db.Area, "area_ID", "area_Name", spot.area_ID);
            return View(spot);
        }

        // GET: Spots/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = await db.Spot.FindAsync(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            ViewBag.area_ID = new SelectList(db.Area, "area_ID", "area_Name", spot.area_ID);
            return View(spot);
        }

        // POST: Spots/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "spot_ID,area_ID,spot_name,spot_tel,spot_context,spot_optimeS,spot_add,spot_fee,spot_score,spot_other,spot_loaction")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spot).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.area_ID = new SelectList(db.Area, "area_ID", "area_Name", spot.area_ID);
            return View(spot);
        }

        // GET: Spots/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Spot spot = await db.Spot.FindAsync(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // POST: Spots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Spot spot = await db.Spot.FindAsync(id);
            db.Spot.Remove(spot);
            await db.SaveChangesAsync();
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
