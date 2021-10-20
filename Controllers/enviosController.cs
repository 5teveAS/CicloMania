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
    public class enviosController : Controller
    {
        private CicloEntities1 db = new CicloEntities1();

        // GET: envios
        public ActionResult Index()
        {
            var envio = db.envio.Include(e => e.factura);
            return View(envio.ToList());
        }

        // GET: envios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            envio envio = db.envio.Find(id);
            if (envio == null)
            {
                return HttpNotFound();
            }
            return View(envio);
        }

        // GET: envios/Create
        public ActionResult Create()
        {
            ViewBag.facturaid = new SelectList(db.factura, "facturaid", "facturaid");
            return View();
        }

        // POST: envios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "envioid,facturaid,direccion,fechaEnvio,estado")] envio envio)
        {
            if (ModelState.IsValid)
            {
                db.envio.Add(envio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.facturaid = new SelectList(db.factura, "facturaid", "facturaid", envio.facturaid);
            return View(envio);
        }

        // GET: envios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            envio envio = db.envio.Find(id);
            if (envio == null)
            {
                return HttpNotFound();
            }
            ViewBag.facturaid = new SelectList(db.factura, "facturaid", "facturaid", envio.facturaid);
            return View(envio);
        }

        // POST: envios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "envioid,facturaid,direccion,fechaEnvio,estado")] envio envio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(envio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.facturaid = new SelectList(db.factura, "facturaid", "facturaid", envio.facturaid);
            return View(envio);
        }

        // GET: envios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            envio envio = db.envio.Find(id);
            if (envio == null)
            {
                return HttpNotFound();
            }
            return View(envio);
        }

        // POST: envios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            envio envio = db.envio.Find(id);
            db.envio.Remove(envio);
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
