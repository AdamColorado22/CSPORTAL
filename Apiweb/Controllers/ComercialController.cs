using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Apiweb.Models;
using Apiweb.Properties;
using Apiweb.Utilidad;
using CrystalDecisions.CrystalReports.Engine;
using DataTables;
using log4net;
using Database = DataTables.Database;

namespace Apiweb.Controllers
{
    public class ComercialController : Controller
    {
        private static readonly ILog Log = Logs.GetLogger();
     

        // GET: WorkFlow
        Settings settings = Properties.Settings.Default;
        string usuario = SessionManager.GetUsuario();
        string entidad = SessionManager.GetEntidad();
        // GET: Comercial
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Listar()
        {

            Log.Info("Listando Lista Comercial");
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new DataTables.Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_COMERCIAL", "ID")
                 .Where(q =>
                    q
                    .Where("ESTADO", "N", "=")
                    //.OrWhere("ESTADO", "R", "=")
                  )
                .Model<TER_COMERCIAL>()
                .Field(new Field("ID"))
                .Field(new Field("PART_ID"))
                .Field(new Field("COTIZACION"))
                .Field(new Field("DUI"))
                .Field(new Field("ANCHO"))
                .Field(new Field("LARGO"))
                .Field(new Field("PESO_TOTAL"))
                .Field(new Field("PESO_MILLAR"))
                .Field(new Field("FC_OTROS"))
                .Field(new Field("UN_X_M2"))
                .Field(new Field("UN_X_KG"))
                .Field(new Field("PESO_X_BOBINA"))
                .Field(new Field("USUARIO")
                .SetValue(usuario))
                .Field(new Field("ESTADO")
                .SetValue("N"))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult ListarDetalle(string ID)
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_DET_COMERCIAL", "ID")
                    .Where(q =>
                    q
                    .Where("ID_COMERCIAL", ID, "=")
                    )
                 .Model<TER_DET_COMERCIAL>()
                .Field(new Field("E_REP"))
                .Field(new Field("E_KG"))
                .Field(new Field("E_ROLL"))
                .Field(new Field("C_KG"))
                .Field(new Field("C_ROLLO"))
                .Field(new Field("PRECIO_R"))
                .Field(new Field("ID_COMERCIAL")
                .SetValue(ID))
                .Field(new Field("PRECIO_2"))
                .Field(new Field("PRECIO_3"))
                .Field(new Field("MC"))
                .Field(new Field("C_REP"))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }






        public ActionResult Download_PDF(string ID,string sr,string att , string tel)
        {
            try
            {
                DataSet ds = new DataSet();
                string constr = settings.DBCONSULTA;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = String.Format("EXEC TER_SP_REP_COMERCIAL {0},'{1}','{2}','{3}' ",ID,sr,att,tel);
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
                .Select(dataRow => new TER_REP_COMERCIAL
                {
                   
                    TITULO = dataRow.Field<string>("TITULO"),
                    COTIZACION = dataRow.Field<string>("COTIZACION"),
                    ATT = dataRow.Field<string>("ATT"),
                    TEL = dataRow.Field<string>("TEL"),
                    PART_ID = dataRow.Field<string>("PART_ID"),
                    ESTRUCTURA = dataRow.Field<string>("ESTRUCTURA"),
                    C_KG = dataRow.Field<string>("C_KG"),
                    PRECIO_R = dataRow.Field<string>("PRECIO_R"),

                }).ToList();

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/Reporte"), "Comercial.rpt"));
                rd.SetDataSource(Data);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();


                rd.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
                rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
                rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA5;

                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "CustomerList.pdf");
            }
            catch (Exception ex) {
                Log.Error(String.Format("Error al Generar reporte {0}", ex.Message.ToString()));
                return View(); }
           

            
        }



        public JsonResult Procesar(string ID)
        {
            try
            {

                string sql = @" EXEC TER_SP_COMERCIAL_PROCESAR @ID";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", Int32.Parse( ID))
                        //new SqlParameter("@ALTERNO", 0)
                        );

                        db.SaveChanges();
                        dbContextTransaction.Commit();



                    }
                }
                DateTime now = DateTime.Now;
                //correo(objeto);
                return Json(data: new { success = true, message = "Datos Ingresados" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Insertar", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult Cerrar(string ID)
        {
            try
            {

                string sql = @" UPDATE TER_COMERCIAL SET ESTADO='C' WHERE ID= @ID";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", Int32.Parse(ID))
                        //new SqlParameter("@ALTERNO", 0)
                        );

                        db.SaveChanges();
                        dbContextTransaction.Commit();



                    }
                }
                DateTime now = DateTime.Now;
                //correo(objeto);
                return Json(data: new { success = true, message = "Propuesta Cerrada" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Cerrar", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}