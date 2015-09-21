using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GetOpenData.Models;
using Newtonsoft.Json;


namespace GetOpenData.Controllers
{
    public class DataController : Controller
    {
        // GET: /Data/

        public async Task<ActionResult> Index()
        {
            string url = "http://data.ntpc.gov.tw/od/data/api/BF90FA7E-C358-4CDA-B579-B6C84ADC96A1?$format=json";

            ViewData["url"] = url;

            HttpClient client = new HttpClient();
            //設定可存取得內容為最大上限
            client.MaxResponseContentBufferSize = Int32.MaxValue;

            TempData["url"] = url;

            //取得json 陣列 以字串方式取回
            var response = await client.GetStringAsync(url);

            // ViewBag
            ViewBag.Result = response;

            var collection = JsonConvert.DeserializeObject<IEnumerable<data>>(response);

            return View(collection);
        }

        //
        // GET: /Data/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Data/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Data/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            } catch
            {
                return View();
            }
        }

        //
        // GET: /Data/Edit/5

        public ActionResult Save()
        {
            return View();
        }

        //
        // POST: /Data/Edit/5

        [HttpPost]
        public ActionResult Save([Bind(Include = "spot_ID,area_ID,spot_name,spot_tel,spot_context,spot_optimeS,spot_optimeE,spot_add,spot_fee,spot_score,spot_other,class_ID")] Spot spot)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            } catch
            {
                return View();
            }
        }

        //
        // GET: /Data/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Data/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            } catch
            {
                return View();
            }
        }
    }
}
