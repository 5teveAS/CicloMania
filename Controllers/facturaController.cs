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
    public class facturaController : Controller
    {
        private CicloEntities1 db = new CicloEntities1();

        // GET: factura
        public ActionResult Index()
        {
            var factura = db.factura.Include(f => f.articulo).Include(f => f.cliente);
            return View(factura.ToList());
        }

        // GET: factura/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            factura factura = db.factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: factura/Create
        public ActionResult Create()
        {
            ViewBag.articuloid = new SelectList(db.articulo, "articuloid", "marca");
            ViewBag.clienteId = new SelectList(db.cliente, "clienteId", "cedula");
            return View();
        }

        // POST: factura/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "facturaid,clienteId,articuloid,cantidad,total")] factura factura)
        {
            if (ModelState.IsValid)
            {
                db.factura.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.articuloid = new SelectList(db.articulo, "articuloid", "marca", factura.articuloid);
            ViewBag.clienteId = new SelectList(db.cliente, "clienteId", "cedula", factura.clienteId);
            return View(factura);
        }

        // GET: factura/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            factura factura = db.factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.articuloid = new SelectList(db.articulo, "articuloid", "marca", factura.articuloid);
            ViewBag.clienteId = new SelectList(db.cliente, "clienteId", "cedula", factura.clienteId);
            return View(factura);
        }

        // POST: factura/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "facturaid,clienteId,articuloid,cantidad,total")] factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.articuloid = new SelectList(db.articulo, "articuloid", "marca", factura.articuloid);
            ViewBag.clienteId = new SelectList(db.cliente, "clienteId", "cedula", factura.clienteId);
            return View(factura);
        }

        // GET: factura/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            factura factura = db.factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: factura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            factura factura = db.factura.Find(id);
            db.factura.Remove(factura);
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
