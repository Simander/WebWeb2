using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nettbutikken2Mvc.Models;

namespace Nettbutikken2Mvc.Controllers
{
    public class AdminVareController : Controller
    {
        private VareDatabaseContext db = new VareDatabaseContext();

        // GET: AdminVare
        public ActionResult Index(string vareKategori, string searchString)
        {
            var kategoriList = new List<string>();

            var kategoriNvn = from d in db.vare
                              orderby d.KategoriNavn
                              select d.KategoriNavn;

            kategoriList.AddRange(kategoriNvn.Distinct());
            ViewBag.vareKategori = new SelectList(kategoriList);

            var dbvare = from m in db.vare
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                dbvare = dbvare.Where(s => s.Navn.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(vareKategori))
            {
                dbvare = dbvare.Where(x => x.KategoriNavn == vareKategori);
            }

            return View(dbvare);
            // return View(db.vare.ToList());
        }

        // GET: AdminVare/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dbVare dbVare = db.vare.Find(id);
            if (dbVare == null)
            {
                return HttpNotFound();
            }
            return View(dbVare);
        }

        // GET: AdminVare/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminVare/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VareID,VareNr,KategoriNavn,Navn,Beskrivelse,Pris,AntallPaLager,VareBildeUrl")] dbVare dbVare)
        {
            if (ModelState.IsValid)
            {
                db.vare.Add(dbVare);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbVare);
        }

        // GET: AdminVare/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dbVare dbVare = db.vare.Find(id);
            if (dbVare == null)
            {
                return HttpNotFound();
            }
            return View(dbVare);
        }

        // POST: AdminVare/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VareID,VareNr,KategoriNavn,Navn,Beskrivelse,Pris,AntallPaLager,VareBildeUrl")] dbVare dbVare)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dbVare).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dbVare);
        }

        // GET: AdminVare/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dbVare dbVare = db.vare.Find(id);
            if (dbVare == null)
            {
                return HttpNotFound();
            }
            return View(dbVare);
        }

        // POST: AdminVare/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dbVare dbVare = db.vare.Find(id);
            db.vare.Remove(dbVare);
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
