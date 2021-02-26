using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AppMVC.Models; 

namespace AppMVC.Controllers
{
    [RoutePrefix("roles")]
    public class RolesController : Controller
    {
        private PruebaDbContext db = new PruebaDbContext();

        [Route("RoleIndex")]
        public ActionResult RoleIndex()
        {
            var model = db.roles.ToList();
            if (Session["RoleID"] != null)
            {
                
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }



        [Route("CreateRole")]
        public ActionResult CreateRole()
        {
            int rl = Convert.ToInt32(Session["RoleID"]);
            if (rl == 1 || rl == 4)
            {
                return View();
            }
                return RedirectToAction("Roleindex");
        }
        [HttpPost]
        public ActionResult CreateRole([Bind(Include = "Id, Nombre, Usuario_Transaccion, Fecha_Transaccion")] Role role)
        {
            role.Usuario_transaccion = Convert.ToString(Session["Username"]);
            role.fecha_transaccion = DateTime.Now;
            db.roles.Add(role);
            db.SaveChanges();
            return RedirectToAction("RoleIndex");
        }
        [Route("roles"),HttpGet]
        public object GetRoles()
        {

            var dato = db.roles.ToList();
            string a = dato.FirstOrDefault().Nombre;
            var json = new JavaScriptSerializer().Serialize(dato);
            return json;
        }

        
        [Route("getrole/{id}"),HttpGet]
        public List<string> GetRolesId(int id)
        {
            var imp = (from i in db.roles
                       where i.ID == id
                       select i.ToString()).ToList();

            var res = db.roles.Select(i => i.Nombre).ToList();
                                    
            

            return imp;
        }

        
        
        [Route("EditRole")]
        public ActionResult EditRole(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.roles.Find(ID);
            if (role == null)
            {
                return HttpNotFound();
            }
            int rl = Convert.ToInt32(Session["RoleID"]);
            if(rl == 1)
            {
                return View(role);
            }
            return RedirectToAction("RoleIndex");
        }
        [HttpPost]
        public ActionResult EditRole([Bind(Include = "ID, Nombre, Usuario_Transaccion, Fecha_Transaccion")] Role Rol)
        {
            if (ModelState.IsValid)
            {
                Rol.Usuario_transaccion = Convert.ToString(Session["Username"]);
                Rol.fecha_transaccion = DateTime.Now;
                db.Entry(Rol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("RoleIndex");
            }
            return View();
           
        }
        
        //[Route("DeleteRoles")]
        //public ActionResult DeleteRoles(int? Id)
        //{
        //    if (Id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Role role = db.roles.Find(Id);z
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    int rl = Convert.ToInt32(Session["RoleID"]);
        //    if (rl == 1)
        //    {
        //        db.roles.Remove(role);
        //        db.SaveChanges();
        //    }

        //    return RedirectToAction("RoleIndex");
        //}

       

    }
}