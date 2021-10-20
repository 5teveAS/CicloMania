using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CicloMania.Models;

namespace CicloMania.Controllers
{
    public class facturaMantenimientoController : Controller
    {
        private CicloEntities db = new CicloEntities();

        // GET: facturaMantenimiento
        public ActionResult Index()
        {
            var facturaMantenimientos = db.facturaMantenimientos.Include(f => f.cliente);
            return View(facturaMantenimientos.ToList());
        }

        // GET: facturaMantenimiento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facturaMantenimientos facturaMantenimientos = db.facturaMantenimientos.Find(id);
            if (facturaMantenimientos == null)
            {
                return HttpNotFound();
            }
            return View(facturaMantenimientos);
        }

        // GET: facturaMantenimiento/Create
        public ActionResult Create()
        {
            ViewBag.clienteId = new SelectList(db.cliente, "clienteId", "cedula");
            return View();
        }

        // POST: facturaMantenimiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "facturaidM,clienteId,detallesArreglo,total")] facturaMantenimientos facturaMantenimientos)
        {
            if (ModelState.IsValid)
            {
                db.facturaMantenimientos.Add(facturaMantenimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.clienteId = new SelectList(db.cliente, "clienteId", "cedula", facturaMantenimientos.clienteId);
            return View(facturaMantenimientos);
        }

        // GET: facturaMantenimiento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facturaMantenimientos facturaMantenimientos = db.facturaMantenimientos.Find(id);
            if (facturaMantenimientos == null)
            {
                return HttpNotFound();
            }
            ViewBag.clienteId = new SelectList(db.cliente, "clienteId", "cedula", facturaMantenimientos.clienteId);
            return View(facturaMantenimientos);
        }

        // POST: facturaMantenimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "facturaidM,clienteId,detallesArreglo,total")] facturaMantenimientos facturaMantenimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturaMantenimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clienteId = new SelectList(db.cliente, "clienteId", "cedula", facturaMantenimientos.clienteId);
            return View(facturaMantenimientos);
        }

        // GET: facturaMantenimiento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            facturaMantenimientos facturaMantenimientos = db.facturaMantenimientos.Find(id);
            if (facturaMantenimientos == null)
            {
                return HttpNotFound();
            }
            return View(facturaMantenimientos);
        }

        // POST: facturaMantenimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            facturaMantenimientos facturaMantenimientos = db.facturaMantenimientos.Find(id);
            db.facturaMantenimientos.Remove(facturaMantenimientos);
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
