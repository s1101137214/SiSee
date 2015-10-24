using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SiSee_v1.Models;
using SiSee_v1.Models.Repository;
using SiSee_v1.Models.ViewModels;

namespace SiSee_v1.Controllers
{
    public class SpotsController : Controller
    {
        private sisdbEntities1 db = new sisdbEntities1();

        private SpotRepository SpotRepository = new SpotRepository();


        // GET: Spots
        public ActionResult Index(string id)
        {
            string searchName = id;

            var spot = SpotRepository.GetAll();

            List<Spot> spotList = new List<Spot>();

            //取得含有搜尋內容的資料
            if (!String.IsNullOrEmpty(searchName))
            {
                TempData["SearchName"] = searchName;

                //取得含有該區域的資料 取得方式有點詭異 之後再改
                List<Spot> spot_area = spot.Where(s => s.Area.area_Name.Contains(searchName)).ToList();

                List<Spot> spot_name = SpotRepository.GetByName(searchName);

                spotList.AddRange(spot_name);

                spotList.AddRange(spot_area);

            }
            else
            {
                TempData["SearchName"] = "全部";

                spotList.AddRange(spot.Take<Spot>(10));
            }

            ViewData["TotalCount"] = spotList.Count();

            return View(spotList);
        }

        // GET: Spots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Spot spot = db.Spot.Find(id);

            List<CommentRecord> commentRecord = db.CommentRecord.Where(s => s.spot_ID == id).ToList();

            if (spot != null)
            {
                SpotDetail spotDetail = new SpotDetail();

                spotDetail.Spot = spot;

                spotDetail.CommentRecords = commentRecord;

                return View(spotDetail);

                //  return View("SpotDetail", spotDetail);

            }
            else
            {
                return HttpNotFound();
            }

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
        public ActionResult Create([Bind(Include = "spot_ID,area_ID,spot_name,spot_tel,spot_context,spot_optimeS,spot_add,spot_fee,spot_score,spot_other,spot_loaction")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Spot.Add(spot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.area_ID = new SelectList(db.Area, "area_ID", "area_Name", spot.area_ID);
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
            return View(spot);
        }

        // POST: Spots/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "spot_ID,area_ID,spot_name,spot_tel,spot_context,spot_optimeS,spot_add,spot_fee,spot_score,spot_other,spot_loaction")] Spot spot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(spot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.area_ID = new SelectList(db.Area, "area_ID", "area_Name", spot.area_ID);
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

        // GET: Spots/BlogSearch
        [HttpGet, ActionName("SearchBlog")]
        public ActionResult SearchBlog(string searchName)
        {
            ViewData["SearchName"] = searchName;

            return View();
        }

    }
}
