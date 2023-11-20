using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apiweb.Models;
using Apiweb.Properties;
using log4net;

namespace Apiweb.Controllers
{
    public class PartDescriptionController : Controller
    {
        private static readonly ILog Log = Logs.GetLogger();
   
        Correo correos = new Correo();
        // GET: WorkFlow
        Settings settings = Properties.Settings.Default;
        string usuario = (System.Web.HttpContext.Current.Session["User"] as String).ToUpper();

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult ListarPartDescription(String Base)
        {


            Log.Info("Listando ce Articulos ");

            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC [TER_SP_LISTAR_PART_DESCRIPTION]'" + Base + "' ";
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            var Data = ds.Tables[0].AsEnumerable()
            .Select(dataRow => new LANG_DESCRIPTION
            {
                part_id = dataRow.Field<string>("part_id"),
                language_id = dataRow.Field<string>("language_id"),
                part_description = dataRow.Field<string>("part_description"),
              


            }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);

        }



        public JsonResult Accion(LANG_DESCRIPTION objeto)
        {
            string mensaje = "";
            bool success = true; // Inicialmente asumimos éxito

            try
            {
                string sql = @"EXEC TER_SP_ACTUALIZAR_PART_DESCRIPTION @BASE_ID, @PART_ID, @DESCRIPCION, @ACCION";

                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        db.Database.ExecuteSqlCommand(
                            sql,
                            new SqlParameter("@BASE_ID", objeto.base_id),
                            new SqlParameter("@PART_ID", objeto.part_id),
                            new SqlParameter("@DESCRIPCION", String.IsNullOrEmpty(objeto.part_description) ? "" : objeto.part_description),
                            new SqlParameter("@ACCION", objeto.accion)
                        );

                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                }

                // Determina el mensaje basado en la acción
                switch (objeto.accion)
                {
                    case "1":
                        mensaje = "Datos Ingresados";
                        break;
                    case "2":
                        mensaje = "Datos Actualizados";
                        break;
                    case  "3":
                        mensaje = "Datos Eliminados";
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error al realizar la operación: {ex.Message}");
                success = false; // Cambia el éxito a falso si hay una excepción
                mensaje = ex.Message; // Usar el mensaje de excepción
            }

            return Json(data: new { success, message = mensaje }, JsonRequestBehavior.AllowGet);
        }



    }
}