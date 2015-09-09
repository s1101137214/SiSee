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
    public class TEST_USERController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: TEST_USER
        public ActionResult Index()
        {
            return View(db.TEST_USER.ToList());
        }

        // GET: TEST_USER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEST_USER tEST_USER = db.TEST_USER.Find(id);
            if (tEST_USER == null)
            {
                return HttpNotFound();
            }
            return View(tEST_USER);
        }

        // GET: TEST_USER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TEST_USER/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USER_ID,USER_Name,USER_Password,USER_Phone")] TEST_USER tEST_USER)
        {
            if (ModelState.IsValid)
            {
                db.TEST_USER.Add(tEST_USER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tEST_USER);
        }

        // GET: TEST_USER/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEST_USER tEST_USER = db.TEST_USER.Find(id);
            if (tEST_USER == null)
            {
                return HttpNotFound();
            }
            return View(tEST_USER);
        }

        // POST: TEST_USER/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER_ID,USER_Name,USER_Password,USER_Phone")] TEST_USER tEST_USER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tEST_USER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tEST_USER);
        }

        // GET: TEST_USER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEST_USER tEST_USER = db.TEST_USER.Find(id);
            if (tEST_USER == null)
            {
                return HttpNotFound();
            }
            return View(tEST_USER);
        }

        // POST: TEST_USER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TEST_USER tEST_USER = db.TEST_USER.Find(id);
            db.TEST_USER.Remove(tEST_USER);
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
