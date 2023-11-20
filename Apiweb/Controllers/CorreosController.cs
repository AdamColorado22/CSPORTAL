using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apiweb.App_Start;
using Apiweb.Models;
using DataTables;

namespace Apiweb.Controllers
{
    [VerificarSesion]
    public class CorreosController : Controller
    {
        // GET: Correos
        

        public ActionResult FormularioCorreo(string tipo, int paso)
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new DataTables.Database("sqlserver", settings.DBConnection))
            {
                var response = new Editor(db, "TER_W_CORREOS", "ID")
                 .Where(q =>
                    q
                    .Where("TIPO", tipo, "=")
                    .AndWhere("PASO_ACTUAL", paso, "=")
                     )
                .Model<TER_W_CORREOS>()
                .Field(new Field("ID"))
                .Field(new Field("USUARIO"))
                .Field(new Field("CORREO"))
                .Field(new Field("TIPO"))
                .Field(new Field("PASO_ACTUAL"))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }
    }
}