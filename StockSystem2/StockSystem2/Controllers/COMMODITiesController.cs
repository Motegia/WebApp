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
    public class COMMODITiesController : Controller
    {
        private trdbEntities db = new trdbEntities();

        // GET: COMMODITies
        public ActionResult Index()
        {
            return View(db.COMMODITY.ToList());
        }

        // GET: COMMODITies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMODITY cOMMODITY = db.COMMODITY.Find(id);
            if (cOMMODITY == null)
            {
                return HttpNotFound();
            }
            return View(cOMMODITY);
        }

        // GET: COMMODITies/Create
        public ActionResult Create()
        {
            return View();
        }
        Models.STOCK sTOCK = new STOCK();
        // POST: COMMODITies/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,PRICE,REGION")] COMMODITY cOMMODITY)
        {
            if (ModelState.IsValid)
            {
                db.COMMODITY.Add(cOMMODITY);
                db.COMMODITY.Add(cOMMODITY);
                db.STOCK.Add(sTOCK);
                sTOCK.ID = cOMMODITY.ID;
                sTOCK.NAME = cOMMODITY.NAME;
                sTOCK.PRICE = cOMMODITY.PRICE;
                sTOCK.QUONTITY = 0;
                sTOCK.SUBTOTAL = 0;
                sTOCK.UPDATEHISTORY = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOMMODITY);
        }

        // GET: COMMODITies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMODITY cOMMODITY = db.COMMODITY.Find(id);
            if (cOMMODITY == null)
            {
                return HttpNotFound();
            }
            return View(cOMMODITY);
        }

        // POST: COMMODITies/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,PRICE,REGION")] COMMODITY cOMMODITY, [Bind(Include = "ID,NAME,PRICE,QUONTITY,SUBTOTAL,UPDATEHISTORY")] STOCK sTOCK)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMMODITY).State = EntityState.Modified;
                sTOCK.NAME = cOMMODITY.NAME;
                sTOCK.PRICE = cOMMODITY.PRICE;
                sTOCK.QUONTITY = (from x in db.STOCK where x.ID == cOMMODITY.ID select x.QUONTITY).Single();
                sTOCK.SUBTOTAL = sTOCK.PRICE * sTOCK.QUONTITY;
                sTOCK.UPDATEHISTORY = DateTime.Now;


                db.Entry(sTOCK).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(cOMMODITY);
        }

        // GET: COMMODITies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMMODITY cOMMODITY = db.COMMODITY.Find(id);
            if (cOMMODITY == null)
            {
                return HttpNotFound();
            }
            return View(cOMMODITY);
        }

        // POST: COMMODITies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            COMMODITY cOMMODITY = db.COMMODITY.Find(id);
            db.COMMODITY.Remove(cOMMODITY);
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
