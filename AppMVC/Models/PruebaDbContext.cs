using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AppMVC.Models
{
    public class PruebaDbContext : DbContext
    {
            public PruebaDbContext()
                : base("name=MVCDb")
            {
            }
            public DbSet<Usuarios> Usuarios { get; set; }
            public DbSet<Role> roles { get; set; }
    }
}