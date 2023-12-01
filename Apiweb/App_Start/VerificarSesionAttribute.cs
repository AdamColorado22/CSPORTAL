using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apiweb.App_Start
{
    public class VerificarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (System.Web.HttpContext.Current.Session["User"] == null || System.Web.HttpContext.Current.Session["Base"] == null)
            {
                // Cerrar la sesión y redirigir a la página de inicio de sesión u otra página
                System.Web.HttpContext.Current.Session.Abandon(); // Cerrar la sesión
                filterContext.Result = new RedirectResult("~/Login/logoff"); // Redirigir a la página de inicio de sesión
            }
        }
    }

}