using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using CicloMania.Models;

namespace CicloMania.Controllers
{
    public class articuloController : Controller
    {
        private CicloEntities1 db = new CicloEntities1();

        // GET: articulo
        public ActionResult Index()
        {
            return View(db.articulo.ToList());
        }

        // GET: articulo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // GET: articulo/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: articulo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "articuloid,marca,nombre,descripcion,imagen,codigoArticulo,precio")] articulo articulo)
        {
            HttpPostedFileBase FileBase = Request.Files[0];
            
            if (FileBase.ContentLength == 0)
            {
                ModelState.AddModelError("Imagen", "Es necesario seleccionar una imagen");
            }
            else
            {
                if (FileBase.FileName.EndsWith(".jpg"))
                {

                    WebImage image = new WebImage(FileBase.InputStream);

                    articulo.imagen = image.GetBytes();
                } else
                {
                    ModelState.AddModelError("Imagen", "El sistema unicamente acepta imagenes con formato JPG.");
                }


            }




            if (ModelState.IsValid)
            {
                db.articulo.Add(articulo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articulo);
        }


        // GET: articulo/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // POST: articulo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "articuloid,marca,nombre,descripcion,imagen,codigoArticulo,precio")] articulo articulo)
        {
           // byte[] imagenActual = null;
            articulo _articulos = new articulo();

            HttpPostedFileBase FileBase = Request.Files[0];

            if (FileBase.ContentLength == 0)
            {
                _articulos = db.articulo.Find(articulo.articuloid);
                articulo.imagen = _articulos.imagen;

            }
            else {


                if (FileBase.FileName.EndsWith(".jpg"))
                {

                    WebImage image = new WebImage(FileBase.InputStream);

                    articulo.imagen = image.GetBytes();
                }
                else
                {
                    ModelState.AddModelError("Imagen", "El sistema unicamente acepta imagenes con formato JPG.");
                }

            }


            if (ModelState.IsValid)
            {
                db.Entry(_articulos).State = EntityState.Detached;
                db.Entry(articulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articulo);
        }

        // GET: articulo/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            articulo articulo = db.articulo.Find(id);
            if (articulo == null)
            {
                return HttpNotFound();
            }
            return View(articulo);
        }

        // POST: articulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            articulo articulo = db.articulo.Find(id);
            db.articulo.Remove(articulo);
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

        // GET: articulo/getImage/5
        public ActionResult getImage(int id)
        {
            articulo articulosk = db.articulo.Find(id);
            byte[] byteImage = articulosk.imagen;

            MemoryStream memoryStream = new MemoryStream(byteImage);
            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }
    }
}
