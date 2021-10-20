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
    public class mantenimientosController : Controller
    {
        private CicloEntities1 db = new CicloEntities1();

        // GET: mantenimientos
        public ActionResult Index()
        {
            return View(db.mantenimientos.ToList());
        }

        // GET: mantenimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mantenimientos mantenimientos = db.mantenimientos.Find(id);
            if (mantenimientos == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientos);
        }

        // GET: mantenimientos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mantenimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Num_Mantenimiento,cedula,tipoMantenimiento,detallesArreglo")] mantenimientos mantenimientos)
        {
            if (ModelState.IsValid)
            {
                db.mantenimientos.Add(mantenimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mantenimientos);
        }

        // GET: mantenimientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mantenimientos mantenimientos = db.mantenimientos.Find(id);
            if (mantenimientos == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientos);
        }

        // POST: mantenimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Num_Mantenimiento,cedula,tipoMantenimiento,detallesArreglo")] mantenimientos mantenimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mantenimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mantenimientos);
        }

        // GET: mantenimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mantenimientos mantenimientos = db.mantenimientos.Find(id);
            if (mantenimientos == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientos);
        }

        // POST: mantenimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mantenimientos mantenimientos = db.mantenimientos.Find(id);
            db.mantenimientos.Remove(mantenimientos);
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
