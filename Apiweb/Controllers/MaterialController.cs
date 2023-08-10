using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apiweb.Models;
using Apiweb.Properties;
using DataTables;
using ExcelDataReader;
using log4net;

namespace Apiweb.Controllers
{
    public class MaterialController : Controller
    {
        //    private static readonly ILog Log = Logs.GetLogger();
        //    AppDbContext _dbContext;
        //    //Correo correos = new Correo();
        //    // GET: WorkFlow
        //    Settings settings = Properties.Settings.Default;
        //    //string usuario = (System.Web.HttpContext.Current.Session["User"] as String).ToUpper();
        //    //string entidad = System.Web.HttpContext.Current.Session["Base"] as String;
        public ActionResult Index()
        {
            return View();
        }


        //    public ActionResult Listar()
        //    {

        //        Log.Info("Listando Lista Comercial");
        //        var formData = HttpContext.Request.Form;
        //        var settings = Properties.Settings.Default;

        //        using (var db = new DataTables.Database("sqlserver", settings.DBConnection))
        //        {


        //            var response = new Editor(db, "TER_MATERIAL", "ID")
        //            //.Where(q =>
        //            //q
        //            //.Where("ESTADO", "GENERADO", "<>")
        //            //.AndWhere("IDUSUARIO", usuario, "=")
        //            //.AndWhere("BASE", entidad, "=")
        //            //)
        //            .Model<TER_MATERIAL>()
        //            .Field(new Field("ID"))
        //            .Field(new Field("PART_ID"))
        //            .Field(new Field("RESINA"))
        //            .Field(new Field("PORCENTAJE"))
        //            .Field(new Field("PROCESO"))
        //            .Process(formData)
        //            .Data();

        //            return Json(response, JsonRequestBehavior.AllowGet);

        //        }

        //    }

        //    [System.Web.Http.HttpPost]
        //    public ActionResult DataImportAll(HttpPostedFileBase excelFile)
        //    {

        //        try
        //        {

        //            HttpPostedFileBase file = Request.Files["FileUpload"]; // Obtener archivos cargados
        //                                                                   // Carga el archivo de Excel y devuelve un DataTable con los datos de las columnas "Nombre" e "ID"
        //            DataTable dataTable = LeerExcel(file);
        //            InsertarDataenSQL(dataTable);


        //        }
        //        catch (Exception e)
        //        {
        //            String msn = e.Message.ToString();
        //            ViewBag.error = msn;
        //            return View("Error");
        //        }

        //        return View("Index");


        //    }


        //    public DataTable LeerExcel(HttpPostedFileBase excelFile)
        //    {
        //        // Crea un DataTable vacío con las columnas "ARTICULO", "RESINAS", "PORCENTAJE" y "PROCESO"
        //        DataTable dataTable = new DataTable();
        //        dataTable.Columns.Add("ARTICULO", typeof(string));
        //        dataTable.Columns.Add("RESINAS", typeof(string));
        //        dataTable.Columns.Add("PORCENTAJE", typeof(decimal));
        //        dataTable.Columns.Add("PROCESO", typeof(string));

        //        // Comprueba si el archivo es nulo o no y si es válido o no
        //        if (excelFile == null || excelFile.ContentLength == 0)
        //        {
        //            ViewBag.ErrorMessage = "Please select a valid file";
        //            return dataTable;
        //        }

        //        if (!excelFile.FileName.EndsWith(".xls") && !excelFile.FileName.EndsWith(".xlsx"))
        //        {
        //            ViewBag.ErrorMessage = "Please select a valid Excel file";
        //            return dataTable;
        //        }

        //        // Lee los datos del archivo de Excel
        //        using (var stream = excelFile.InputStream)
        //        {
        //            using (var reader = ExcelReaderFactory.CreateReader(stream))
        //            {
        //                var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
        //                {
        //                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
        //                    {
        //                        UseHeaderRow = true
        //                    }
        //                });

        //                var excelDataTable = dataSet.Tables[0];

        //                // Recorre las filas de la tabla y lee los datos de las columnas "ARTICULO", "RESINAS", "PORCENTAJE" y "PROCESO"
        //                foreach (DataRow excelDataRow in excelDataTable.Rows)
        //                {
        //                    DataRow dataRow = dataTable.NewRow();
        //                    dataRow["ARTICULO"] = excelDataRow["ARTICULO"].ToString();
        //                    dataRow["RESINAS"] = excelDataRow["RESINAS"].ToString();
        //                    dataRow["PORCENTAJE"] = decimal.Parse(excelDataRow["PORCENTAJE"].ToString());
        //                    dataRow["PROCESO"] = excelDataRow["PROCESO"].ToString();
        //                    dataTable.Rows.Add(dataRow);
        //                }
        //            }
        //        }

        //        return dataTable;
        //    }


        //    public void InsertarDataenSQL(DataTable dt)
        //    {
        //        using (SqlConnection connection = new SqlConnection(settings.DBConnection))
        //        {
        //            connection.Open();
        //            using (SqlTransaction transaction = connection.BeginTransaction())
        //            {
        //                try
        //                {
        //                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
        //                    {
        //                        bulkCopy.DestinationTableName = "TER_MATERIAL";
        //                        bulkCopy.ColumnMappings.Add("ARTICULO", "PART_ID");
        //                        bulkCopy.ColumnMappings.Add("RESINAS", "RESINA");
        //                        bulkCopy.ColumnMappings.Add("PORCENTAJE", "PORCENTAJE");
        //                        bulkCopy.ColumnMappings.Add("PROCESO", "PROCESO");
        //                        bulkCopy.WriteToServer(dt);
        //                    }
        //                    transaction.Commit();
        //                    Console.WriteLine("Datos Insertados.");
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(ex.Message);
        //                    transaction.Rollback();
        //                }
        //            }
        //            connection.Close();
        //        } }

        //    public JsonResult Actualizar()
        //    {
        //        bool respuesta = true;
        //        try
        //        {
        //            using (TERMOAPPEntities db = new TERMOAPPEntities())
        //            {


        //                var data = db.Database.ExecuteSqlCommand(@"exec TER_ACTUALIZAR_MAESTROS");



        //            }
        //        }
        //        catch(Exception ex)
        //        {
        //            return Json(data: new { success = false, message = $"Error al abrir el archivo {ex}" }, JsonRequestBehavior.AllowGet);
        //        }



        //        return Json(data: new { success = true, message = $"Procesado" }, JsonRequestBehavior.AllowGet);
        //    }





    }
}