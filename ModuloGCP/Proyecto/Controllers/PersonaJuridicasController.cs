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
    public class PersonaJuridicasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonaJuridicas
        public ActionResult Index()
        {
            return View(db.PersonaJuridica.ToList());
        }

        // GET: PersonaJuridicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonaJuridica personaJuridica = db.PersonaJuridica.Find(id);
            if (personaJuridica == null)
            {
                return HttpNotFound();
            }
            return View(personaJuridica);
        }

        // GET: PersonaJuridicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonaJuridicas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,telefono,Direccion,Correo,ruc,razon_social,representante")] PersonaJuridica personaJuridica)
        {
            if (ModelState.IsValid)
            {
                db.PersonaJuridica.Add(personaJuridica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personaJuridica);
        }

        // GET: PersonaJuridicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonaJuridica personaJuridica = db.PersonaJuridica.Find(id);
            if (personaJuridica == null)
            {
                return HttpNotFound();
            }
            return View(personaJuridica);
        }

        // POST: PersonaJuridicas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,telefono,Direccion,Correo,ruc,razon_social,representante")] PersonaJuridica personaJuridica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personaJuridica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personaJuridica);
        }

        // GET: PersonaJuridicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonaJuridica personaJuridica = db.PersonaJuridica.Find(id);
            if (personaJuridica == null)
            {
                return HttpNotFound();
            }
            return View(personaJuridica);
        }

        // POST: PersonaJuridicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonaJuridica personaJuridica = db.PersonaJuridica.Find(id);
            db.PersonaJuridica.Remove(personaJuridica);
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
