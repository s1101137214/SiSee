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

namespace SiSee_v1.Controllers
{
    public class FavoriteRecordsController : Controller
    {
        private sisdbEntities1 db = new sisdbEntities1();

        private SpotRepository SpotRepository = new SpotRepository();

        // GET: FavoriteRecords
        public ActionResult Index()
        {
            var favoriteRecord = db.FavoriteRecord.Include(f => f.Spot).Include(f => f.User);
            return View(favoriteRecord.ToList());
        }

        // GET: FavoriteRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteRecord favoriteRecord = db.FavoriteRecord.Find(id);
            if (favoriteRecord == null)
            {
                return HttpNotFound();
            }
            return View(favoriteRecord);
        }

        // GET: FavoriteRecords/CreateFavoriteRecord
        public bool CreateFavoriteRecord(int id)
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return false;
            }

            FavoriteRecord searchReacord = new FavoriteRecord()
            {
                spot_ID = id,
                user_ID = String.IsNullOrEmpty(User.Identity.Name) ? 1 : int.Parse(User.Identity.Name)
            };

            SpotRepository.CreateFavoriteReacord(searchReacord);

            return true;
        }

        // GET: FavoriteRecords/CheckFavoriteRecord
        public bool CheckFavoriteRecord(int id)
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return false;
            }

            if (!SpotRepository.GetSpotFavoriteRecordIsSet(id, User.Identity.Name))
            {
                return false;
            }

            return true;
        }

        // GET: FavoriteRecords/CheckFavoriteRecord
        public bool DeleteFavoriteRecord(int id)
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return false;
            }

            SpotRepository.DeleteFavoriteRecord(id, User.Identity.Name);

            return true;
        }

        // GET: FavoriteRecords/Create
        public ActionResult Create()
        {
            ViewBag.spot_ID = new SelectList(db.Spot, "spot_ID", "spot_name");
            ViewBag.user_ID = new SelectList(db.User, "user_ID", "user_FBID");
            return View();
        }

        // POST: FavoriteRecords/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "searchrecord_ID,spot_ID,user_ID")] FavoriteRecord favoriteRecord)
        {
            if (ModelState.IsValid)
            {
                db.FavoriteRecord.Add(favoriteRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.spot_ID = new SelectList(db.Spot, "spot_ID", "spot_name", favoriteRecord.spot_ID);
            ViewBag.user_ID = new SelectList(db.User, "user_ID", "user_FBID", favoriteRecord.user_ID);
            return View(favoriteRecord);
        }

        // GET: FavoriteRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteRecord favoriteRecord = db.FavoriteRecord.Find(id);
            if (favoriteRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.spot_ID = new SelectList(db.Spot, "spot_ID", "spot_name", favoriteRecord.spot_ID);
            ViewBag.user_ID = new SelectList(db.User, "user_ID", "user_FBID", favoriteRecord.user_ID);
            return View(favoriteRecord);
        }

        // POST: FavoriteRecords/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "searchrecord_ID,spot_ID,user_ID")] FavoriteRecord favoriteRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favoriteRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.spot_ID = new SelectList(db.Spot, "spot_ID", "spot_name", favoriteRecord.spot_ID);
            ViewBag.user_ID = new SelectList(db.User, "user_ID", "user_FBID", favoriteRecord.user_ID);
            return View(favoriteRecord);
        }

        // GET: FavoriteRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteRecord favoriteRecord = db.FavoriteRecord.Find(id);
            if (favoriteRecord == null)
            {
                return HttpNotFound();
            }
            return View(favoriteRecord);
        }

        // POST: FavoriteRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FavoriteRecord favoriteRecord = db.FavoriteRecord.Find(id);
            db.FavoriteRecord.Remove(favoriteRecord);
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
