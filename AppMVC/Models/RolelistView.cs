using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppMVC.Models
{
    public class RolelistView
    {
        [NotMapped]
        public List<Usuarios> RolesUs { get; set; }
    }
}