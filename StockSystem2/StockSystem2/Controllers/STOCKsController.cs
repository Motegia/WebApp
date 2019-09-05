using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockSystem2.Models;

namespace StockSystem2.Controllers
{
    public class STOCKsController : Controller
    {
        private trdbEntities db = new trdbEntities();

        // GET: STOCKs
        public ActionResult Index()
        {
            return View(db.STOCK.ToList());
        }

        // GET: STOCKs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK sTOCK = db.STOCK.Find(id);
            if (sTOCK == null)
            {
                return HttpNotFound();
            }
            return View(sTOCK);
        }

        // GET: STOCKs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: STOCKs/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,PRICE,QUONTITY,SUBTOTAL,UPDATEHISTORY")] STOCK sTOCK)
        {
            if (ModelState.IsValid)
            {
                db.STOCK.Add(sTOCK);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sTOCK);
        }

        // GET: STOCKs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK sTOCK = db.STOCK.Find(id);
            if (sTOCK == null)
            {
                return HttpNotFound();
            }
            return View(sTOCK);
        }

        // POST: STOCKs/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,PRICE,QUONTITY,SUBTOTAL,UPDATEHISTORY")] STOCK sTOCK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTOCK).State = EntityState.Modified;
                sTOCK.SUBTOTAL = sTOCK.PRICE * sTOCK.QUONTITY;
                sTOCK.UPDATEHISTORY = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTOCK);
        }

        // GET: STOCKs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK sTOCK = db.STOCK.Find(id);
            if (sTOCK == null)
            {
                return HttpNotFound();
            }
            return View(sTOCK);
        }

        // POST: STOCKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            STOCK sTOCK = db.STOCK.Find(id);
            db.STOCK.Remove(sTOCK);
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
