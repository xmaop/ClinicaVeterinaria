﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using System.IO;

namespace Proyecto.Controllers
{
    public class PacientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pacients
        public ActionResult Index()
        {
            var pacients = db.Pacients.Include(p => p.Client);
            return View(pacients.ToList());
        }

        // GET: Pacients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacients.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }

        // GET: Pacients/Create
        public ActionResult Create(int? ClientId)
        {
            try
            {
                Client client = db.Clients.Find(ClientId);
                ViewBag.ClientId = client;
            }
            catch (System.InvalidOperationException ex)
            {
                PersonaJuridica client = db.PersonaJuridica.Find(ClientId);
                ViewBag.ClientId = client;
            }
            
            //ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Nombre");
            //ViewBag.PersonaJ = new SelectList(db.PersonaJuridica, "ClientId", "razon_social");
            return View();
        }

        // POST: Pacients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PacientId,nombre,ClientId,FechaNac,Genero,Peso,FechaCese,Color,Estado")] Pacient pacient, HttpPostedFileBase file)
        {
            

                if (ModelState.IsValid)
                {

                    int lastPacientId = db.Pacients.Max(item => item.PacientId);
                    pacient.Foto = lastPacientId.ToString() + ".jpg";
                    db.Pacients.Add(pacient);
                    db.SaveChanges();
                    
                if (file != null && file.ContentLength > 0)
                    {
                        //string pic = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/images/profile"), pacient.Foto + ".jpg");
                        // file is uploaded
                        file.SaveAs(path);
                    }
                return RedirectToAction("Index");
                }
            
            

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Direccion", pacient.ClientId);
            return View(pacient);
        }

        public ActionResult Filtra(int? id)
        {
            var detcalendario = db.Pacients.Where(d => d.ClientId == id);
            return View(detcalendario.ToList());
        }

        // GET: Pacients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacients.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Direccion", pacient.ClientId);
            return View(pacient);
        }

        // POST: Pacients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PacientId,nombre,ClientId,FechaNac,Genero,Foto,Peso,FechaCese,Color,Estado")] Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "Direccion", pacient.ClientId);
            return View(pacient);
        }

        // GET: Pacients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacient pacient = db.Pacients.Find(id);
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }

        // POST: Pacients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pacient pacient = db.Pacients.Find(id);
            db.Pacients.Remove(pacient);
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
