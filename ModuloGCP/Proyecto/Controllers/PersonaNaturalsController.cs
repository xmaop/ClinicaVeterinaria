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
    public class PersonaNaturalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonaNaturals
        public ActionResult Index()
        {
            return View(db.Clients.ToList());
        }

        // GET: PersonaNaturals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonaNatural personaNatural = db.Clients.Find(id);
            if (personaNatural == null)
            {
                return HttpNotFound();
            }
            return View(personaNatural);
        }

        // GET: PersonaNaturals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonaNaturals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,telefono,Direccion,Correo,Nombre,Apellido_Paterno,Apellido_Materno,FechaNac")] PersonaNatural personaNatural)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(personaNatural);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personaNatural);
        }

        // GET: PersonaNaturals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonaNatural personaNatural = db.Clients.Find(id);
            if (personaNatural == null)
            {
                return HttpNotFound();
            }
            return View(personaNatural);
        }

        // POST: PersonaNaturals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,telefono,Direccion,Correo,Nombre,Apellido_Paterno,Apellido_Materno,FechaNac")] PersonaNatural personaNatural)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personaNatural).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personaNatural);
        }

        // GET: PersonaNaturals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonaNatural personaNatural = db.Clients.Find(id);
            if (personaNatural == null)
            {
                return HttpNotFound();
            }
            return View(personaNatural);
        }

        // POST: PersonaNaturals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonaNatural personaNatural = db.Clients.Find(id);
            db.Clients.Remove(personaNatural);
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
