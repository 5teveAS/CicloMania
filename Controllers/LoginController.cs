
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CicloMania.Clases;
using CicloMania.Models;

namespace CicloMania.Controllers
{
    public class LoginController : Controller
    {
        USUARIOS usuario = new USUARIOS();
        // GET: Ingresar
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(USUARIOS usuario, string url)
        {
            if (IsValid(usuario))
            {
                /* Si el usuario es valido se genera un Cookie  con FALSE para que no persista */
                FormsAuthentication.SetAuthCookie(usuario.Nombre_de_usuario, false);
                if (url != null)
                {
                    /* Si el usuario es valido y si la URL es diferente de nulo */
                    return Redirect(url);
                }
                /* Si no se va al home de la aplicación */
                return RedirectToAction("Index", "Home");
            }
            TempData["Mensaje"] = "Credenciales Incorrectas.";
            return View(usuario);
        }

        public bool IsValid(USUARIOS usuario)
        {
            return usuario.Autententicar();
        }
        public ActionResult LogOut(Usuarios usuario, string url)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Guardar(Usuarios modelo)
        {
            ViewBag.Mensaje = "";
            return View(modelo);
        }

        public ActionResult Nuevo(Usuarios modelo)
        {
            usuario.Guardar(modelo);
            ViewBag.Mensaje = "El registro del usuario se guardo correctamente";
            return View("Guardar", modelo);
        }
        //private readonly CicloEntities1 db = new CicloEntities1();
        //// GET: Login
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(Usuarios user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bool IsValidUser = db.Usuarios
        //       .Any(u => u.Nombre_de_usuario.ToLower() == user
        //       .Nombre_de_usuario.ToLower() && db.Usuarios
        //       .Any(j => j.C_Contraseña_.ToLower() == user.C_Contraseña_.ToLower()));

        //        if (IsValidUser)
        //        {
        //            FormsAuthentication.SetAuthCookie(user.Nombre_de_usuario, false);
        //            return RedirectToAction("Index", "Home");
        //        }

        //    }

        //   ModelState.AddModelError("", "invalid Username or Password");
        //    return View();
        //}

        //   public ActionResult Register()
        //   {
        //       return View();
        //   }

        //   [HttpPost]  
        //   [ValidateAntiForgeryToken]  
        //public ActionResult Register(Usuarios registerUser)
        //{  
        //      if (ModelState.IsValid)  
        //       {  
        //      db.Usuarios.Add(registerUser);  
        //         db.SaveChanges();  
        //         return RedirectToAction("Login");  

        //        }
        //      return View();
        //  }

        // public ActionResult Logout()
        // {  
        // FormsAuthentication.SignOut();  
        // return RedirectToAction("Login");  
        //}
    }
}