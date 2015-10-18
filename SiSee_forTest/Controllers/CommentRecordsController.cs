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
    public class CommentRecordsController : Controller
    {
        private sisdbEntities1 db = new sisdbEntities1();

        // GET: CommentRecords
        public ActionResult Index()
        {
            var commentRecord = db.CommentRecord.Include(c => c.Spot).Include(c => c.User);
            return View(commentRecord.ToList());
        }

        // GET: CommentRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentRecord commentRecord = db.CommentRecord.Find(id);
            if (commentRecord == null)
            {
                return HttpNotFound();
            }
            return View(commentRecord);
        }

        // GET: CommentRecords/Create
        public ActionResult Create()
        {
            ViewBag.spot_ID = new SelectList(db.Spot, "spot_ID", "spot_name");
            ViewBag.user_ID = new SelectList(db.User, "user_ID", "user_name");
            return View();
        }

        // POST: CommentRecords/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "commentrecord_ID,spot_ID,user_ID,comment_context,comment_grade,comment_date")] CommentRecord commentRecord)
        {
            if (ModelState.IsValid)
            {
                db.CommentRecord.Add(commentRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.spot_ID = new SelectList(db.Spot, "spot_ID", "spot_name", commentRecord.spot_ID);
            ViewBag.user_ID = new SelectList(db.User, "user_ID", "user_name", commentRecord.user_ID);
            return View(commentRecord);
        }

        // GET: CommentRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentRecord commentRecord = db.CommentRecord.Find(id);
            if (commentRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.spot_ID = new SelectList(db.Spot, "spot_ID", "spot_name", commentRecord.spot_ID);
            ViewBag.user_ID = new SelectList(db.User, "user_ID", "user_name", commentRecord.user_ID);
            return View(commentRecord);
        }

        // POST: CommentRecords/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentrecord_ID,spot_ID,user_ID,comment_context,comment_grade,comment_date")] CommentRecord commentRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.spot_ID = new SelectList(db.Spot, "spot_ID", "spot_name", commentRecord.spot_ID);
            ViewBag.user_ID = new SelectList(db.User, "user_ID", "user_name", commentRecord.user_ID);
            return View(commentRecord);
        }

        // GET: CommentRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentRecord commentRecord = db.CommentRecord.Find(id);
            if (commentRecord == null)
            {
                return HttpNotFound();
            }
            return View(commentRecord);
        }

        // POST: CommentRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentRecord commentRecord = db.CommentRecord.Find(id);
            db.CommentRecord.Remove(commentRecord);
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
