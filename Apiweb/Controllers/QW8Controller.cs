using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apiweb.Filtro;
using Apiweb.Models;
using Apiweb.Properties;
using DataTables;
namespace Apiweb.Controllers
{
    public class QW8Controller : Controller
    {
        // GET: QW8
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Listar()
        {
            String usuario = System.Web.HttpContext.Current.Session["User"] as String;
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;
            var update = "dd/MM/yyyy";
            using (var db = new Database("sqlserver", settings.DBTERMO))
            {


                var response = new Editor(db, "TER_PORC_TEXTIL", "ID")
                .Model<TER_PORC_TEXTIL>()
                .Field(new Field("ID"))
                .Field(new Field("TIPO")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                .Field(new Field("CLIENTE"))
                .Field(new Field("ARTICULO"))
                .Field(new Field("PORCENTAJE")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("Activo")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("FECHA_DESDE"))
                .Field(new Field("FECHA_HASTA")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("USUARIO")
                .SetValue(usuario))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }


        public JsonResult Historial(string id)
        {
            int ID = Int32.Parse(id);
            List <TER_PORC_TEXTIL_HIST> oLstCliente = new List<TER_PORC_TEXTIL_HIST>();

            using (TERMOEntities db = new TERMOEntities())
            {

                oLstCliente = (from p in db.TER_PORC_TEXTIL_HIST.Where(x => x.ID_E == ID)
                               select p).ToList();

                return Json(new { data = oLstCliente }, JsonRequestBehavior.AllowGet);
            }



        }
    }
}