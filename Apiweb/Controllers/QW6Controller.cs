using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apiweb.Filtro;
using Apiweb.Models;
using Apiweb.Properties;
using DataTables;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Apiweb.Controllers
{
    [VerificaSesion]
    public class QW6Controller : Controller
    {
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
        // GET: QW6
        Settings settings = Properties.Settings.Default;
        public ActionResult Index()
        {
            
            ViewBag.Base = System.Web.HttpContext.Current.Session["Base"] as String;
            ViewBag.Usuario = System.Web.HttpContext.Current.Session["User"] as String;
            ValidarRol();
            if (ColocacionOC)
            { return View(); }
            else
            { return View("Error"); }
        }


        public ActionResult Plantilla()
        {

            ValidarRol();
            if (ColocacionPedido)
            { return View(); }
            else
            { return View("Error"); }
        }


        public ActionResult Listar()
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {
                

                var response = new Editor(db, "TER_QW6_PLANTILLA","ID")
                 .Where(q =>
                    q
                    .Where("ESTADO", "C", "!=")
                    .Where("USUARIO", System.Web.HttpContext.Current.Session["User"] as String)
                    .Where("BASE", System.Web.HttpContext.Current.Session["Base"] as String)
                    .OrWhere("ESTADO", null)
                    .Where("USUARIO", System.Web.HttpContext.Current.Session["User"] as String)
                    .Where("BASE", System.Web.HttpContext.Current.Session["Base"] as String)
                    )
                .Model<TER_QW6_PLANTILLA>()
                .Field(new Field("ID"))
                .Field(new Field("PO_NUMBER")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                .Field(new Field("CLIENT"))
                .Field(new Field("CUSTOMER_PART_ID") )
                .Field(new Field("QUANTITY")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRECIO")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("COMENTARIO")
                .SetFormatter(Format.IfEmpty(null)))
                //.Validator(Validation.DateFormat(
                //            update,
                //            new ValidationOpts { Message = "Please enter a date in the format yyyy-mm-dd" }
                //            ))
                //        .GetFormatter(Format.DateSqlToFormat(update))
                //        .SetFormatter(Format.DateFormatToSql(update))
                //    )
                .Field(new Field("PART_ID")
                .SetFormatter(Format.IfEmpty(null)))
                 .Field(new Field("STAGE_ID"))
               .Field(new Field("ESTADO")
                .SetFormatter(Format.IfEmpty(null)))
               .Field(new Field("ENCABEZADO")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("MONEDA")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("OC")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("USUARIO")
                .SetValue(System.Web.HttpContext.Current.Session["User"] as String))
                .Field(new Field("BASE")
                .SetValue(System.Web.HttpContext.Current.Session["Base"] as String))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }
        public ActionResult Listar2()
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_QW6_PLANTILLA_PED","ID")
                    .Where(q =>
                    q
                    .Where("ESTADO", "C", "!=")
                    .Where("USUARIO", System.Web.HttpContext.Current.Session["User"] as String)
                    .Where("BASE", System.Web.HttpContext.Current.Session["Base"] as String)
                    .OrWhere("ESTADO",null)
                    .Where("USUARIO", System.Web.HttpContext.Current.Session["User"] as String)
                    .Where("BASE", System.Web.HttpContext.Current.Session["Base"] as String)
                    )
                    //.Where("ESTADO", "C", "!=")
                    //.Where("USUARIO", System.Web.HttpContext.Current.Session["User"] as String)
                    //.Where("BASE", System.Web.HttpContext.Current.Session["Base"] as String)
                .Model<TER_QW6_PLANTILLA_PED>()
                .Field(new Field("ID"))
                .Field(new Field("CUSTOMER_ID")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                .Field(new Field("PART_ID"))
                .Field(new Field("STAGE_ID"))
                .Field(new Field("CUSTOMER_PART_ID"))
                .Field(new Field("QUANTITY")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRICE")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("ESTADO")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("MONEDA")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("ENCABEZADO")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("PEDIDO")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("USUARIO")
                .SetValue(System.Web.HttpContext.Current.Session["User"] as String))
                .Field(new Field("BASE")
                .SetValue(System.Web.HttpContext.Current.Session["Base"] as String))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }



        public JsonResult Inventario(string item)
        {

            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC TER_TRACE @ITEM, @BASE";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@ITEM", SqlDbType.VarChar, 30).Value = "%" + item + "%";
                    cmd.Parameters.Add("@BASE", SqlDbType.VarChar, 30).Value = System.Web.HttpContext.Current.Session["Base"] as String;

                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            var empList = ds.Tables[0].AsEnumerable()
            .Select(dataRow => new Inventarios
            {
                PART_ID = dataRow.Field<string>("PART_ID"),
                ITEM = dataRow.Field<string>("USER_10"),
                INVENTARIO = dataRow.Field<Decimal>("INVENTARIO"),
                DESCRIPCION = dataRow.Field<string>("APROPERTY_1"),
                FECHA = dataRow.Field<string>("APROPERTY_3"),
                CAJA = dataRow.Field<string>("CAJA"),
                TRACE_ID = dataRow.Field<string>("TRACE_ID"),
                STAGE_ID = dataRow.Field<string>("STAGE_ID")
            }).ToList();


            return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
        }





        public JsonResult Articulo(string item)
        {

            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC TER_ARTICULO @ITEM, @BASE";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@ITEM", SqlDbType.VarChar, 30).Value = "%" + item + "%";
                    cmd.Parameters.Add("@BASE", SqlDbType.VarChar, 30).Value = System.Web.HttpContext.Current.Session["Base"] as String;

                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            var empList = ds.Tables[0].AsEnumerable()
            .Select(dataRow => new Articulo
            {
                PART_ID = dataRow.Field<string>("PART_ID"),
                DESCRIPTION = dataRow.Field<string>("DESCRIPTION"),
                INVENTARIO = dataRow.Field<Decimal>("INVENTARIO"),
                USER_10 = dataRow.Field<string>("USER_10"),
                STAGE_ID = dataRow.Field<string>("STAGE_ID"),
                
            }).ToList();


            return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Procesar(string Part_id, string ITEM)
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec [dbo].[TER_CPART_ID] '" + Part_id + "','" + ITEM + "'");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        public JsonResult AutoComplete(string valor)
        {
            Array array;
            IList<String> intermediate_list = new List<String>();
            List<Cliente> custlist = null;
            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC TER_CUSTOMER  @BASE,@VALOR";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    
                    cmd.Parameters.Add("@BASE", SqlDbType.VarChar, 30).Value = System.Web.HttpContext.Current.Session["Base"] as String;
                    cmd.Parameters.Add("@VALOR", SqlDbType.VarChar, 30).Value = "%" + valor + "%";
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            custlist = new List<Cliente>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Cliente cobj = new Cliente();
                cobj.ID = ds.Tables[0].Rows[i]["ID"].ToString();
                cobj.NOMBRE = ds.Tables[0].Rows[i]["NOMBRE"].ToString();
                 custlist.Add(cobj);
            }

            return Json(custlist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutoCompleteV(string valor)
        {
            Array array;
            IList<String> intermediate_list = new List<String>();
            List<Cliente> custlist = null;
            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC [TER_VENDOR]  @BASE,@VALOR";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@BASE", SqlDbType.VarChar, 30).Value = System.Web.HttpContext.Current.Session["Base"] as String;
                    cmd.Parameters.Add("@VALOR", SqlDbType.VarChar, 30).Value = "%" + valor + "%";
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            custlist = new List<Cliente>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Cliente cobj = new Cliente();
                cobj.ID = ds.Tables[0].Rows[i]["ID"].ToString();
                cobj.NOMBRE = ds.Tables[0].Rows[i]["NOMBRE"].ToString();
                custlist.Add(cobj);
            }

            return Json(custlist, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Procesar2(string Part_id, string ITEM)
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec [dbo].[TER_CPPART_ID] '" + Part_id + "','" + ITEM + "'");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DataImportPurc(HttpPostedFileBase filebase)
        {
            string valor;
            bool flage = false;
            try
            {

                HttpPostedFileBase file = Request.Files["FileUpload"]; // Obtener archivos cargados


                if (file == null || file.ContentLength < 0 || file.FileName == "")
                {
                    //ViewBag.error = "El archivo no puede estar vacío";
                    return View("Index");

                }
                else
                {
                    // Verifica la extensión del archivo
                    string filename = Path.GetFileName(file.FileName);
                    string fileEx = System.IO.Path.GetExtension(file.FileName);
                    // Obtenga el nombre del sufijo requerido
                    string FileType = ".xlsx";
                    if (!FileType.Contains(fileEx))
                    {
                        ViewBag.error = "El formato de archivo es incorrecto";
                        return View("Error");
                    }
                    Stream inputStream = file.InputStream;

                    XSSFWorkbook hssfworkbook = new XSSFWorkbook(inputStream);

                    // HSSFWorkbook hssfworkbook = new HSSFWorkbook(inputStream);
                    NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0); // La primera hoja

                    int rowCount = sheet.LastRowNum;

                    for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        TER_QW6_PLANTILLA smi = new TER_QW6_PLANTILLA();
                        if (row != null)
                        {
                            using (TERMOAPPEntities db = new TERMOAPPEntities())
                            {

                                if (row.GetCell(0) != null)
                                {

                                    if (!row.GetCell(0).ToString().Equals(""))
                                    {
                                        smi.ENCABEZADO = row.GetCell(0) == null ? "" : row.GetCell(0).ToString().Replace(" ", String.Empty);
                                        smi.PO_NUMBER = row.GetCell(1) == null ? "" : row.GetCell(1).ToString().Replace(" ", String.Empty);
                                        smi.CLIENT = row.GetCell(2) == null ? "" : row.GetCell(2).ToString().Replace(" ", String.Empty);
                                        smi.CUSTOMER_PART_ID = row.GetCell(3) == null ? "" : row.GetCell(3).ToString().Replace(" ", String.Empty);
                                        smi.PART_ID = row.GetCell(4) == null ? "" : row.GetCell(4).ToString().Replace(" ", String.Empty);
                                        smi.QUANTITY = (decimal)double.Parse(row.GetCell(5) == null ? "0" : row.GetCell(5).ToString().Replace(" ", String.Empty));
                                        smi.PRECIO = (decimal)double.Parse(row.GetCell(6) == null ? "0" : row.GetCell(6).ToString().Replace(" ", String.Empty));
                                        smi.COMENTARIO = row.GetCell(7) == null ? "" : row.GetCell(7).ToString().Replace(" ", String.Empty);
                                        smi.ESTADO = "A";
                                        smi.USUARIO = System.Web.HttpContext.Current.Session["User"] as String;
                                        smi.BASE = System.Web.HttpContext.Current.Session["Base"] as String;
                                        db.TER_QW6_PLANTILLA.Add(smi);
                                        db.SaveChanges();
                                        flage = true;
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                String msn = e.Message.ToString();
                ViewBag.error = msn;
                return View("Error");
            }
            if (flage == true)
            {
                ViewBag.link = "Index";
                ViewBag.ruta = "QW6";

                return View("Inicio");
            }
            else
            {
                ViewBag.error = "Archivo con Errores";
                return View("Error");
            }



        }


        public ActionResult DataImportPed(HttpPostedFileBase filebase)
        {
            string valor;
            bool flage = false;
            try
            {

                HttpPostedFileBase file = Request.Files["FileUpload"]; // Obtener archivos cargados


                if (file == null || file.ContentLength < 0 || file.FileName == "")
                {
                    //ViewBag.error = "El archivo no puede estar vacío";
                    return View("Index");

                }
                else
                {
                    // Verifica la extensión del archivo
                    string filename = Path.GetFileName(file.FileName);
                    string fileEx = System.IO.Path.GetExtension(file.FileName);
                    // Obtenga el nombre del sufijo requerido
                    string FileType = ".xlsx";
                    if (!FileType.Contains(fileEx))
                    {
                        ViewBag.error = "El formato de archivo es incorrecto";
                        return View("Error");
                    }
                    Stream inputStream = file.InputStream;

                    XSSFWorkbook hssfworkbook = new XSSFWorkbook(inputStream);

                    // HSSFWorkbook hssfworkbook = new HSSFWorkbook(inputStream);
                    NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0); // La primera hoja

                    int rowCount = sheet.LastRowNum;

                    for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        TER_QW6_PLANTILLA_PED smi = new TER_QW6_PLANTILLA_PED();
                        if (row != null)
                        {
                            using (TERMOAPPEntities db = new TERMOAPPEntities())
                            {

                                if (row.GetCell(0) != null)
                                {
                                    if (!row.GetCell(0).ToString().Equals(""))
                                    {
                                        smi.ENCABEZADO = row.GetCell(0) == null ? "" : row.GetCell(0).ToString().Replace(" ", String.Empty);
                                        smi.CUSTOMER_ID = row.GetCell(1) == null ? "" : row.GetCell(1).ToString().Replace(" ", String.Empty);
                                        smi.PART_ID = row.GetCell(2) == null ? "" : row.GetCell(2).ToString().Replace(" ", String.Empty);
                                        smi.CUSTOMER_PART_ID = row.GetCell(3) == null ? "" : row.GetCell(3).ToString().Replace(" ", String.Empty);
                                        smi.QUANTITY = (decimal)double.Parse(row.GetCell(4) == null ? "0" : row.GetCell(4).ToString().Replace(" ", String.Empty));
                                        smi.PRICE = (decimal)double.Parse(row.GetCell(5) == null ? "0" : row.GetCell(5).ToString().Replace(" ", String.Empty));
                                        smi.MONEDA = row.GetCell(6) == null ? "" : row.GetCell(6).ToString().Replace(" ", String.Empty);
                                        smi.ESTADO = "A";
                                        smi.USUARIO = System.Web.HttpContext.Current.Session["User"] as String;
                                        smi.BASE = System.Web.HttpContext.Current.Session["Base"] as String;
                                        db.TER_QW6_PLANTILLA_PED.Add(smi);
                                        db.SaveChanges();
                                        flage = true;
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                String msn = e.Message.ToString();
                ViewBag.error = msn;
                return View("Error");
            }
            if (flage == true)
            {
                ViewBag.link = "Plantilla";
                ViewBag.ruta = "QW6";

                return View("Inicio");
            }
            else
            {
                ViewBag.error = "Archivo con Errores";
                return View("Error");
            }



        }

        public ActionResult Inicio()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        public JsonResult ValidacionPO()
        {
            String Base = System.Web.HttpContext.Current.Session["Base"] as String;
            String Usuario = System.Web.HttpContext.Current.Session["User"] as String;
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec TER_PEDIDOS_VALIDAR '" + Base + "','" + Usuario + "'");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrdenarPO()
        {
            String Base = System.Web.HttpContext.Current.Session["Base"] as String;
            String Usuario = System.Web.HttpContext.Current.Session["User"] as String;
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec TER_PEDIDOS '" + Base + "','" + Usuario + "'");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidacionOC()
        {
            String Base = System.Web.HttpContext.Current.Session["Base"] as String;
            String Usuario = System.Web.HttpContext.Current.Session["User"] as String;
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec TER_COMPRAS'" + Base + "','" + Usuario + "'");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }





    public JsonResult ValidacionOCV()
    {
            String Base = System.Web.HttpContext.Current.Session["Base"] as String;
            String Usuario = System.Web.HttpContext.Current.Session["User"] as String;
            bool respuesta = true;
        try
        {
            using (TERMOAPPEntities db = new TERMOAPPEntities())
            {


                var data = db.Database.ExecuteSqlCommand(@"exec TER_COMPRAS_VALIDAR'" + Base + "','" + Usuario + "'");



            }
        }
        catch
        {
            respuesta = false;
        }



        return Json(respuesta, JsonRequestBehavior.AllowGet);
    }

}
}


