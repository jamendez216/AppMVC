using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppMVC.Models;


namespace AppMVC.Controllers
{
    [RoutePrefix("Usuario")]
    public class UsuariosController : Controller
    {
        PruebaDbContext db = new PruebaDbContext();
        [Route("UsuariosIndex")]
        //[Authorize]
        public ActionResult UsuariosIndex()
        {
            var model = db.Usuarios.Include(x => x.role).ToList();
            if(Session["RoleID"] != null)
            {
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }        
        [HttpPost]
        public ActionResult verify(Usuarios usu)
        {           
            return RedirectToAction("Index", "Home");
        }
        [Route("CreateUser")]
        public ActionResult CreateUser()
        {
            Usuarios pb = new Usuarios();           
            pb.RolesLs = db.roles.ToList<Role>();
            int rl = Convert.ToInt32(Session["RoleID"]);
            if (rl == 1 || rl ==4)
            {
                return View(pb);
            }
            return RedirectToAction("UsuariosIndex");
        }
        [HttpPost]
        public ActionResult CreateUser([Bind(Include = "Id, RoleId, Nombre, Apellidos, Cedula, UserName, passwd, Fecha_Nacimiento, Usuario_Transaccion, Fecha_Transaccion")]Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                var valid = (from x in db.Usuarios
                             where x.Cedula == usuario.Cedula
                             select x).ToList();

                var vali2 = db.Usuarios.Where(x => x.Cedula == usuario.Cedula);

                if (valid.Count == 0)
                {
                    usuario.Fecha_Transaccion = DateTime.Now;
                    usuario.Usuario_Transaccion = Convert.ToString(Session["Username"]);
                    db.Usuarios.Add(usuario);
                    if(usuario.RoleId == 1)
                    {
                        return RedirectToAction("CreateUser");
                    }
                    else
                    {
                        db.SaveChanges();
                        return RedirectToAction("UsuariosIndex");
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
        [Route("EditUser")]        
        public ActionResult EditUser(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(Id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            int rl = Convert.ToInt32(Session["RoleID"]);
            string usr = Convert.ToString(Session["Username"]);
            if (rl == 1 || usr == usuarios.UserName)
            {
                return View(usuarios);
            }
            return RedirectToAction("UsuariosIndex");          
        }
        [HttpPost]
        public ActionResult EditUser([Bind(Include = "Id, RoleId, Nombre, Apellidos, Cedula, UserName, passwd, Fecha_Nacimiento, Usuario_Transaccion, Fecha_Transaccion")]Usuarios usuario)
        {
            int rl = Convert.ToInt32(Session["RoleID"]);
            if (ModelState.IsValid)
            {
                usuario.RoleId = rl;
                usuario.Usuario_Transaccion = Convert.ToString(Session["Username"]);
                usuario.Fecha_Transaccion = DateTime.Now;
                usuario.Cedula = usuario.Cedula;
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UsuariosIndex");
            }
            return View();
        }
       [Route("DeleteUser/{Id}")]       
        public ActionResult DeleteUser(int Id)
        {
            int rl = Convert.ToInt32(Session["RoleID"]);
            if(rl == 1)
            {
                Usuarios usuarios = db.Usuarios.Find(Id);
                db.Usuarios.Remove(usuarios);
                db.SaveChanges();
                return RedirectToAction("UsuariosIndex");
            }
            return RedirectToAction("UsuariosIndex");          
        }
    }
}
