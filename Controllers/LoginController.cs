using CicloMania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CicloMania.Controllers
{
    public class LoginController : Controller
    {
        private readonly CicloEntities1 db = new CicloEntities1();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuarios user)
        {
            if (ModelState.IsValid)
            {
                bool IsValidUser = db.Usuarios
               .Any(u => u.Nombre_de_usuario.ToLower() == user
               .Nombre_de_usuario.ToLower() && user
               .C_Contraseña_ == user.C_Contraseña_);

                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(user.Nombre_de_usuario, false);
                    return RedirectToAction("Index", "Home");
                }

            }
            ModelState.AddModelError("", "invalid Username or Password");
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]  
        [ValidateAntiForgeryToken]  
	    public ActionResult Register(Usuarios registerUser)
	    {  
           if (ModelState.IsValid)  
            {  
	          db.Usuarios.Add(registerUser);  
              db.SaveChanges();  
              return RedirectToAction("Login");  
 
	            }
           return View();
       }

        public ActionResult Logout()
        {  
        FormsAuthentication.SignOut();  
        return RedirectToAction("Login");  
       }
    }
}