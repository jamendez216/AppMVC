using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AppMVC.Models;

namespace AppMVC.Controllers
{
   
    public class HomeController : Controller
    {

        PruebaDbContext db = new PruebaDbContext();

        public ActionResult Index(Usuarios usu)
        {
                if (Session["RoleID"] == null)
                {
                    return View();

                }
                return RedirectToAction("Index", "Home");

        }
        [HttpPost]        
        public ActionResult login(Usuarios usu)
        {
            
            var creds = db.Usuarios.FirstOrDefault(x => x.UserName == usu.UserName && x.passwd == usu.passwd);
            if (creds != null)
            {
               // FormsAuthentication.SetAuthCookie(creds.UserName, false);
                var rolid = (from i in db.Usuarios
                             where usu.UserName == i.UserName && usu.passwd == i.passwd
                             select i.RoleId).FirstOrDefault();
                Session["Username"] = usu.UserName;
                Session["RoleID"] = rolid;
                Session["LogAlert"] = "Valid";
                return RedirectToAction("UsuariosIndex", "Usuarios");
            }

            Session["LogAlert"] = "invalid";            
            return RedirectToAction("Index","Home");
            
        }
        [Route("Home/logout")]
        
        public ActionResult logout()
        {
            Session.Abandon();
            
            return RedirectToAction("Index");
        }

      
    }
}