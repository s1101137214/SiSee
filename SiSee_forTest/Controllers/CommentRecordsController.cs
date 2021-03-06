﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SiSee_v1.Models;
using SiSee_v1.Models.Repository;

namespace SiSee_v1.Controllers
{
    public class CommentRecordsController : Controller
    {
        private AmazonDB db = new AmazonDB();

        private SpotRepository SpotRepository = new SpotRepository();

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

        //自訂新增評論
        [HttpPost]
        public ActionResult CreateCommandRecord(FormCollection data)
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("Details", "Spots", new { id = data["Spot.spot_ID"] });
            }

            CommentRecord commentRecord = new CommentRecord()
            {
                spot_ID = int.Parse(data["Spot.spot_ID"]),
                comment_context = data["Command"],
                comment_grade = data["Grade"],
                user_ID = int.Parse(User.Identity.Name),
                comment_date = System.DateTime.Now
            };

            //SpotRepository.CreateCommand(commentRecord) 
            //暫先用linq新增
            db.CommentRecord.Add(commentRecord);

            db.SaveChanges();

            return RedirectToAction("Details", "Spots", new { id = data["Spot.spot_ID"] });
        }

        // GET: CommentRecords/CheckCommentRecordsCount
        public int? CheckCommentRecordsCount(int id)
        {
            return SpotRepository.GetSpotCommentRecordsCount(id);
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

            Spot spot = db.Spot.Where(s => s.spot_ID == commentRecord.spot_ID).First();
            ViewData["SpotName"] = spot.spot_name;

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
            var id = User.Identity.Name;

            commentRecord.comment_date = System.DateTime.Now;
            commentRecord.user_ID = int.Parse(id);

            if (ModelState.IsValid)
            {
                db.Entry(commentRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyComment", "Users");
            }
            ViewBag.spot_ID = new SelectList(db.Spot, "spot_ID", "spot_name", commentRecord.spot_ID);
            ViewBag.user_ID = new SelectList(db.User, "user_ID", "user_name", commentRecord.user_ID);

            return RedirectToAction("MyComment", "Users");
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
            return RedirectToAction("Details", "Users", id);
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
