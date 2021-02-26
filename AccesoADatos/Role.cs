using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppMVC.Models
{
    [Table("Roles")]
    public class Role
    {


        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        public string Usuario_transaccion { get; set; }

        public DateTime? fecha_transaccion { get; set; }

        


    }
}