using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class GCP_OrdenAtencionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GCP_OrdenAtencion
        public ActionResult Index()
        {
            return View(db.GCP_OrdenAtencion.ToList());
        }

        // GET: GCP_OrdenAtencion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GCP_OrdenAtencion gCP_OrdenAtencion = db.GCP_OrdenAtencion.Find(id);
            if (gCP_OrdenAtencion == null)
            {
                return HttpNotFound();
            }
            return View(gCP_OrdenAtencion);
        }

        // GET: GCP_OrdenAtencion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GCP_OrdenAtencion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,telefono,Direccion,Correo,Estado")] GCP_OrdenAtencion gCP_OrdenAtencion)
        {
            if (ModelState.IsValid)
            {
                db.GCP_OrdenAtencion.Add(gCP_OrdenAtencion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gCP_OrdenAtencion);
        }

        // GET: GCP_OrdenAtencion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GCP_OrdenAtencion gCP_OrdenAtencion = db.GCP_OrdenAtencion.Find(id);
            if (gCP_OrdenAtencion == null)
            {
                return HttpNotFound();
            }
            return View(gCP_OrdenAtencion);
        }

        // POST: GCP_OrdenAtencion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,telefono,Direccion,Correo,Estado")] GCP_OrdenAtencion gCP_OrdenAtencion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gCP_OrdenAtencion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gCP_OrdenAtencion);
        }

        // GET: GCP_OrdenAtencion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GCP_OrdenAtencion gCP_OrdenAtencion = db.GCP_OrdenAtencion.Find(id);
            if (gCP_OrdenAtencion == null)
            {
                return HttpNotFound();
            }
            return View(gCP_OrdenAtencion);
        }

        // POST: GCP_OrdenAtencion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GCP_OrdenAtencion gCP_OrdenAtencion = db.GCP_OrdenAtencion.Find(id);
            db.GCP_OrdenAtencion.Remove(gCP_OrdenAtencion);
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
