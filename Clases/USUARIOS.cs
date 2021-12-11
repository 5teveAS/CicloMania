using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CicloMania.Models;

namespace CicloMania.Clases
{
    public class USUARIOS
    {

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public USUARIOS()
        //{
        //    this.UserRolesMapping = new HashSet<UserRolesMapping>();
        //}

        public int Id { get; set; }
        [Display(Name = "Nombre de Usuario")]
        [Required]
        public string Nombre_de_usuario { get; set; }
        [Display(Name = "Contraseña")]
        [Required]
        public string C_Contraseña_ { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRolesMapping> UserRolesMapping { get; set; }

        CicloEntities1 db = new CicloEntities1();

        public bool Autententicar()
        {
            return db.Usuarios.Where(u => u.Nombre_de_usuario == this.Nombre_de_usuario
            && u.C_Contraseña_ == this.C_Contraseña_)
            .FirstOrDefault() != null;

        }

        public void Guardar(Usuarios modelo)
        {
            db.Usuarios.Add(modelo);
            db.SaveChanges();
        }
    }
}