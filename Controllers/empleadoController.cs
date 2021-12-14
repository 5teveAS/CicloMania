using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CicloMania.Clases;
using CicloMania.Models;

namespace CicloMania.Controllers
{
    public class empleadoController : Controller
    {
        private CicloEntities1 db = new CicloEntities1();

        PersonaEMP persona = new PersonaEMP();
        // GET: empleado
        public ActionResult Index()
        {
           //IEnumerable<empleado> lst = persona.Consultar();

            return View(db.empleado.ToList());
        }

        // GET: empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: empleado/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Mensaje = "";
            return View();
        }

        // POST: empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpleado,cedula,nombre,apellido1,apellido2,puesto,fechaIngreso,telefono,genero,correo,NumCuenta,sueldo")] empleado empleado)
        {
            
            if (ModelState.IsValid)
            {
                db.empleado.Add(empleado);
                db.SaveChanges();
                ViewBag.Mensaje = "El registro se guardo correctamente";
                return RedirectToAction("Index");
            }

            return View(empleado);
        }

        // GET: empleado/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            ViewBag.Mensaje = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmpleado,cedula,nombre,apellido1,apellido2,puesto,fechaIngreso,telefono,genero,correo,NumCuenta,sueldo")] empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Mensaje = "El registro se guardo correctamente";
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        // GET: empleado/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            ViewBag.Mensaje = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            empleado empleado = db.empleado.Find(id);
            db.empleado.Remove(empleado);
            db.SaveChanges();
            ViewBag.Mensaje = "El registro se elimino correctamente";
           return RedirectToAction("Index");
            //return View(empleado);
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
