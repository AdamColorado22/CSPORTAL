using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Utilidad
{
   public static class SessionManager
{
    public static string GetUsuario()
    {
        return (System.Web.HttpContext.Current.Session["User"] as string)?.ToUpper() ?? "defaultUser";
    }

    public static string GetEntidad()
    {
        return System.Web.HttpContext.Current.Session["Base"] as string ?? "defaultBase";
    }
}

}