using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SiSee_v1.Models;
using SiSee_v1.Models.Repository;
using SiSee_v1.Models.ViewModels;

namespace SiSee_v1.Controllers
{
    public class UsersController : Controller
    {
        private sisdbEntities1 db = new sisdbEntities1();

        private UserRepository UserRepository = new UserRepository();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.User.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Index", "Spots");
            }

            User user = db.User.Find(id);

            UserDetail userDetails = new UserDetail()
            {
                User = user
            };

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(userDetails);
        }

        // GET: Comment
        public ActionResult MyComment()
        {
            int id = int.Parse(User.Identity.Name);

            var commentRecord = db.CommentRecord.Where(s => s.user_ID == id).Include(c => c.Spot).Include(c => c.User);

            ViewData["SpotCount"] = "目前有" + commentRecord.Count() + "個評論";

            if (commentRecord.Count() == 0)
            {
                ViewData["SpotCount"] = "目前還沒有評論唷！趕快找到喜歡的景點加入評論！";
            }

            return View(commentRecord);
        }

        // GET: MyFavorite
        public ActionResult MyFavorite()
        {
            int id = int.Parse(User.Identity.Name);

            var favoriteRecord = db.FavoriteRecord.Where(s => s.user_ID == id).Include(c => c.Spot).Include(c => c.User);

            ViewData["SpotCount"] = "目前有" + favoriteRecord.Count() + "個收藏"; 

            if (favoriteRecord.Count() == 0)
            {
                ViewData["SpotCount"] = "目前還沒有收藏唷！趕快找到喜歡的景點加入收藏！";
            }

            return View(favoriteRecord);
        }

        public ActionResult CancelFavoriteRecord(int id)
        {
            var delTarget = db.FavoriteRecord.Find(id);

            db.FavoriteRecord.Remove(delTarget);

            db.SaveChanges();

            return RedirectToAction("Favorite", "Users");
        }

        // POST: Users/CreateByFB
        [HttpPost]
        public String CreateByFB(string id, string name, string email)
        {
            User user = new User()
            {
                user_FBID = id,
                user_name = name,
                user_email = email
            };

            try
            {
                UserRepository.CreateUser(user);

                CheckLogined(id);

            } catch (Exception e)
            {
                return e.ToString();
            }

            return null;
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Spots");
        }

        public bool CheckLogined(string id)
        {
            Session.RemoveAll();

            var user = db.User.Where(u => u.user_FBID.Contains(id)).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            var userID = user.user_ID.ToString();

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                userID,//你想要存放在 User.Identy.Name 的值，通常是使用者帳號
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                true,//將管理者登入的 Cookie 設定成 Session Cookie
               userID,//userdata看你想存放啥
                FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);

            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            return true;
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_ID,user_FBID,user_email,user_tel,user_birth,user_name,user_other")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(User user)
        {
            UserRepository.UpdateUser(user);

            return RedirectToAction("Details", "Users", new { @id = user.user_ID });
        }
        // POST: Users/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_ID,user_FBID,user_email,user_tel,user_birth,user_name,user_other")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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
