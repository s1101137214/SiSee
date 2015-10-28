using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
                ViewData["SearchName"] = searchName;

                //取得含有該區域的資料 取得方式有點詭異 之後再改
                List<Spot> spot_area = spot.Where(s => s.Area.area_Name.Contains(searchName)).ToList();

                List<Spot> spot_name = SpotRepository.GetByName(searchName);

                spotList.AddRange(spot_name);

                spotList.AddRange(spot_area);

            }
            else
            {
                //ViewData["SearchName"] = "全部";

                //暫時取前十筆
                spotList.AddRange(spot.Take<Spot>(10));
            }

            ViewData["TotalCount"] = spotList.Count();

            return View(spotList);
        }

        //判斷景點資料是否為空
        private string SetSpotValueNull(string target)
        {
            if (String.IsNullOrEmpty(target))
                return "未提供";
            return target;
        }

        // GET: Spots/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Spot spot = db.Spot.Find(id);

            List<CommentRecord> commentRecord = db.CommentRecord.Where(s => s.spot_ID == id).OrderBy(s => s.comment_date).ToList();

            if (spot != null)
            {
                SpotDetail spotDetail = new SpotDetail();

                ViewData["SearchName"] = spot.spot_name;

                spot.spot_add = SetSpotValueNull(spot.spot_add);
                spot.spot_context = SetSpotValueNull(spot.spot_context);
                spot.spot_fee = String.IsNullOrEmpty(spot.spot_fee) ? "免費" : spot.spot_fee;
                spot.spot_optimeS = String.IsNullOrEmpty(spot.spot_optimeS) ? "全天開放" : spot.spot_optimeS;
                spot.spot_tel = SetSpotValueNull(spot.spot_tel);
                spot.spot_score = SpotRepository.GetSpotScore(id);

                spotDetail.Spot = spot;

                spotDetail.CommentRecords = commentRecord;

                //新增瀏覽次數 預設user_ID=1 未登入時
                SearchRecord searchReacord = new SearchRecord()
                {
                    spot_ID = id,
                    search_date = System.DateTime.Now,
                    user_ID = String.IsNullOrEmpty(User.Identity.Name) ? 1 : int.Parse(User.Identity.Name)
                };

                ViewData["SearchCount"] = db.SearchRecord.Where(s => s.Spot.spot_ID == id).Count();


                SpotRepository.CreateSearchReacord(searchReacord);

                return View(spotDetail);

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

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult PathPlan()
        {
            return View();
        }

    }
}
