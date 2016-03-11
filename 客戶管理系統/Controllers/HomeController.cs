using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 客戶管理系統.Models;

namespace 客戶管理系統.Controllers {
    public class HomeController : Controller {

        private 客戶資料Entities db = new 客戶資料Entities();

        public ActionResult Index() {
            List<View_客戶清單數量> list = db.View_客戶清單數量.ToList();
            return View(list);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";
            return View();
        }

    }
}