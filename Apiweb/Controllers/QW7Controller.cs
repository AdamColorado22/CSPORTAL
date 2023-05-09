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
using Dapper;
using DataTables;
using log4net;

namespace Apiweb.Controllers
{
  
    [VerificaSesion]
 
    public class QW7Controller : Controller
    {
        private static readonly ILog Log = Logs.GetLogger();
        Properties.Settings settings = Properties.Settings.Default;
        Boolean PedidoDominicana = false;
        Boolean PreciosSMI = false;
        Boolean PreciosMH = false;
        Boolean Historico = false;
        Boolean TexportSocks = false;
        Boolean ColocacionOC = false;
        Boolean ColocacionPedido = false;
        Boolean CortosProduccion = false;

        public void ValidarRol()
        {
            List<Apiweb.Models.USUARIO_ROL> ROl = System.Web.HttpContext.Current.Session["Rol"] as List<Apiweb.Models.USUARIO_ROL>;


            foreach (var item in ROl)
            {
                switch (item.ROL_ID)
                {
                    case 2:
                        PedidoDominicana = true;
                        break;
                    case 3:
                        PreciosSMI = true;
                        break;
                    case 4:
                        PreciosMH = true;
                        break;
                    case 5:
                        Historico = true;
                        break;
                    case 6:
                        TexportSocks = true;
                        break;
                    case 7:
                        ColocacionOC = true;
                        break;
                    case 8:
                        ColocacionPedido = true;
                        break;
                    case 9:
                        CortosProduccion = true;
                        break;

                }
            }

        }

        String usuario = System.Web.HttpContext.Current.Session["User"] as String;

        
       
        public ActionResult Index()
        { 
            ViewBag.Base = System.Web.HttpContext.Current.Session["Base"] as String;
            ViewBag.Usuario = usuario;
            ValidarRol();
            if (CortosProduccion)
            { return View(); }
            else
            { return View("Error"); }
        }


        public ActionResult Listar()
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;
            var update = "dd/MM/yyyy";
            using (var db = new Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_QW7_SEGUIMIETO")

                .Model<TER_QW7_SEGUIMIETO>()
                .Field(new Field("ID"))
                .Field(new Field("NUM_PEDIDO")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                .Field(new Field("PO"))
                .Field(new Field("OT"))
                .Field(new Field("PART_ID"))
                .Field(new Field("CANTIDAD_SOLICITADA")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("CANTIDAD_ENTREGADA")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("CUMPLIMIENTO")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("FECHA_PROMESA"))
                .Field(new Field("FECHA")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("CANTIDAD")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("CAUSA")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("ESTADO_PEDIDO")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("FACTURA")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("PRIORIDAD")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("USUARIO")
                .SetValue(usuario))
                .Field(new Field("FECHA_PED")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("ESTADO")
                .SetFormatter(Format.IfEmpty(null)))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult ConsultaTrazabilidad()
        {
            return View();
        }

            public JsonResult Validacion(string Inicio, string Final, string Estado)
        {
            
            try
            {
                string sql = @"EXEC TER_SP_VALIDARQW7 @INICIO ,@FINAL,@ESTADO";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {

                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@INICIO", Inicio),
                        new SqlParameter("@FINAL", Final),
                         new SqlParameter("@ESTADO", Estado));

                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, message = "Tabla Actualizada" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Historial(string ped)
        {

            List<TER_QW7_SEGUIMIETO_CAMBIOS> oLstCliente = new List<TER_QW7_SEGUIMIETO_CAMBIOS>();

            using (TERMOAPPEntities db = new TERMOAPPEntities())
            {

                oLstCliente = (from p in db.TER_QW7_SEGUIMIETO_CAMBIOS.Where(x => x.PEDIDO == ped)
                               select p).ToList();
            }
            return Json(new { data = oLstCliente }, JsonRequestBehavior.AllowGet);
        }




        public JsonResult Trazabilidad()
        {
            var tabla = new List<Trazabilidad>();

            Log.Info(String.Format("Procesando Trazabilidad usuario {0} ", usuario));
            try
            {



                using (var connection = new SqlConnection(Settings.Default.DBConnection))
                {
                    var query = "EXEC TER_SP_TRAZABILIDAD";
                    tabla = connection.Query<Trazabilidad>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error :{0}", ex.Message.ToString()));
                return Json(new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
            var json =   Json(new { data = tabla }, JsonRequestBehavior.AllowGet);
         
            json.MaxJsonLength = 500000000;
            return json;
        }

    }
}