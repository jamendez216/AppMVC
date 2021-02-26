using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace AppMVC.Models
{
    [Table("Usuarios")]
    public class Usuarios
    {
        public int Id { get; set; }


        [Required]
        public int RoleId { get; set; }


        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }


        [Required]
        [StringLength(40)]
        public string Apellidos { get; set; }


        [Required]
        public string Cedula { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }


        [Required]
        [StringLength(30)]
        public string passwd { get; set; }


        //[Required(ErrorMessage = "es necesario confirmar el password")]
        //[StringLength(30)]
        //[Compare("passwd", ErrorMessage = "No coinciden")]
        //[NotMapped]
        //public string confirmpasswd { get; set; }

        [Required]
        public DateTime Fecha_Nacimiento { get; set; }


        public string Usuario_Transaccion { get; set; }


        public DateTime? Fecha_Transaccion { get; set; }


        public Role role { get; set; }

        [NotMapped]
        public List<Role> RolesLs { get; set; }
        
    }
}