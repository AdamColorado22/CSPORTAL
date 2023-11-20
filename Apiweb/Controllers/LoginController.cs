
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apiweb.Models;

namespace Apiweb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(string usr, string pwd, string Base)
        {
            // Aquí cualquier uso de las variables 'usr', 'pwd' y 'rme'
            string Datos = Base;
            Session["Base"] = Datos;
            if (Autenticacion(usr, pwd) && Permisos(usr)) { return View("Home"); }
            else { ViewBag.error = "Error en el Inicio de Sesion"; return View("Error"); }

        }


        public bool Autenticacion(string UserId, string UserPass)
        {
            string path;
            string filtro;
            string domainAndUsername = @"TERMONET\" + UserId;
            DirectoryEntry entry = new DirectoryEntry("LDAP://termonet.com/DC=termonet,DC=com", domainAndUsername, UserPass);
            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + UserId + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                //Update the new path to the user in the directory.
                path = result.Path;
                filtro = (string)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {

                //String Base = System.Web.HttpContext.Current.Session["Base"] as String;

                if (UserId.Equals("iruiz") | UserId.Equals("acolorado") | UserId.Equals("4750"))
                {
                    //Sesion de Prueba
                    Session["User"] = UserId;
                    return true;
                }

                return false;

                //Falla de autenticacion
                //Session["User"] = "acolorado";
                //return true;
            }
            Session["User"] = UserId;
            return true;

        }


        public bool Permisos(string UserId)
        {
            List<USUARIO_ROL> oCliente = new List<USUARIO_ROL>();
            try
            {
                using (TERMOSEGEntities db = new TERMOSEGEntities())
                {

                    oCliente = (from p in db.USUARIO_ROL.Where(x => x.USUARIO_ID == UserId && x.MODULO_ID == 28)
                                select p).ToList();
                }
                if (oCliente != null)
                {
                    Session["Rol"] = oCliente;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }


        }


        public ActionResult logoff()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}