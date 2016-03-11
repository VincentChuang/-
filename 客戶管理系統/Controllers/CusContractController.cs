using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using 客戶管理系統.Models;

namespace 客戶管理系統.Controllers {
    public class CusContractController : Controller {

        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: CusContract
        public ActionResult Index(string keyword, int cusId = 999) {
            var data = db.客戶聯絡人
                .Include(客 => 客.客戶資料)
                .Where(x => x.是否已刪除 == false);
            if (keyword != null && keyword != "") {
                data = data.Where(x => x.姓名.Contains(keyword));
            }
            if (cusId != 999) {
                data = data.Where(x=>x.客戶Id==cusId);
            }
            ViewBag.keyword = keyword;
            ViewBag.cusId = cusId;
            return View(data.ToList());
        }

        // GET: CusContract/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null) {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }


        //* 實作「客戶聯絡人」時，同一個客戶下的聯絡人，其 Email 不能重複。
        private bool bln判斷同個客戶聯絡人郵件不可重複(客戶聯絡人 contract) {
            bool result = false;
            result = db.客戶聯絡人
                .Where(x => x.客戶Id == contract.客戶Id)
                .Where(x => x.Email == contract.Email).Any();
            result = !result;
            return result;
        }

        // GET: CusContract/Create
        public ActionResult Create() {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }
        // POST: CusContract/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人) {
            if (ModelState.IsValid && bln判斷同個客戶聯絡人郵件不可重複(客戶聯絡人)) {
                db.客戶聯絡人.Add(客戶聯絡人);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: CusContract/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null) {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }
        // POST: CusContract/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人) {
            if (ModelState.IsValid && bln判斷同個客戶聯絡人郵件不可重複(客戶聯絡人)) {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: CusContract/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null) {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: CusContract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            客戶聯絡人.是否已刪除 = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
