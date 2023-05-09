
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
namespace Apiweb.Filtro
{
    public class VerificaSesion : ActionFilterAttribute,
     IAuthenticationFilter
    {
        private bool _auth;
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            _auth = (filterContext.ActionDescriptor.GetCustomAttributes(
                typeof(OverrideAuthenticationAttribute), true).Length == 0);
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var oUsuario = (String)HttpContext.Current.Session["User"];

            if (oUsuario == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult()

                {
                    ViewName = "Error"
                };
            }

        }
    }
}