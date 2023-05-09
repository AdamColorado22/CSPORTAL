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
    public class QW2Controller : Controller
    {
        // GET: QW2
        Boolean PedidoDominicana = false;
        Boolean PreciosSMI = false;
        Boolean PreciosMH = false;
        Boolean Historico = false;
        Boolean TexportSocks = false;
        Boolean ColocacionOC = false;
        Boolean ColocacionPedido = false;
        Boolean CortosProduccion = false;
        Boolean Underwear = false;
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
                    case 10:
                        Underwear = true;
                        break;

                }
            }

        }

        Settings settings = Properties.Settings.Default;
        public ActionResult Index()
        {
            ValidarRol();
            if (TexportSocks)
            { return View(); }
            else
            { return View("Error"); }
        }
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Precios()
        {
            return View();
        }


        public ActionResult Under()
        {
            ValidarRol();
            if (Underwear)
            { return View(); }
            else
            { return View("Error"); }
        }


        public ActionResult UnderMH()
        {
            ValidarRol();
            if (Underwear)
            { return View(); }
            else
            { return View("Error"); }
        }
        public ActionResult Listar()
        {
            var formData = HttpContext.Request.Form;
            //var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {
                var update = "yyyy-MM-dd";

                var response = new Editor(db, "TER_QW2_HBI", "ID")
                .Model<TER_QW2_HBI>()
                .Field(new Field("ID"))
                .Field(new Field("CODES")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                .Field(new Field("PO"))
                .Field(new Field("PRECIO_SV")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("HBI_TIL")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("SAFETY_SCTOCK")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("ON_HAND_CORDER")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("ON_HAND_SMI")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("QTY")
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("PERCENT_SMI")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("QTY_PROCESS")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("QTY_RED")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("DATE_INV_W")
                .SetFormatter(Format.IfEmpty(null)))
                //.Validator(Validation.DateFormat(
                //            update,
                //            new ValidationOpts { Message = "Please enter a date in the format yyyy-mm-dd" }
                //            ))
                //        .GetFormatter(Format.DateSqlToFormat(update))
                //        .SetFormatter(Format.DateFormatToSql(update))
                //    )
                .Field(new Field("VENDOR_COMENTS")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("ESTADO")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("EDIT"))
                .Field(new Field("REVISION")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("ORDEN")
                .SetFormatter(Format.IfEmpty(null)))
                
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }



        public ActionResult ListarU()
        {
            var formData = HttpContext.Request.Form;
            //var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {
                var update = "yyyy-MM-dd";

                var response = new Editor(db, "TER_QW2_UNDERWEAR","ID")
                .Model<TER_QW2_UNDERWEAR>()
                .Field(new Field("ID"))
                .Field(new Field("HBI_NUMBER")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                .Field(new Field("ACTIVE_TILS")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("SUPLIER")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("WIP")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("FGWIP")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("PRICING")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("TRANSITO1")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("SEMANA")
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("TRANSITO2")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("TRANSITO3")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("TRANSITO4")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("CASE_QTY")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("MAX_EXPOSURE")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("CTNS")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("NRQ")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRODUCTION_DATE")
                .SetFormatter(Format.IfEmpty(null)))
               
                .Field(new Field("COMMENTS")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("ESTADO")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("IDENTIFICADOR")
                .SetFormatter(Format.IfEmpty(null)))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }


        [System.Web.Http.HttpPost]
        public ActionResult DataImportAll(HttpPostedFileBase filebase)
        {
            string valor;
            bool flage = false;
            HttpPostedFileBase file = Request.Files["FileUpload"]; // Obtener archivos cargados
            try
            {

                if (file == null || file.ContentLength < 0 || file.FileName == "")
                {
                    return View("Index");
                }
                else
                {
                    // Verifica la extensión del archivo
                    string filename = Path.GetFileName(file.FileName);
                    string fileEx = System.IO.Path.GetExtension(filename);
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
                        TER_QW2_HBI smi = new TER_QW2_HBI();
                        if (row != null)
                        {
                            using (TERMOAPPEntities db = new TERMOAPPEntities())
                            {

                                if (row.GetCell(0) != null)
                                {


                                    smi.CODES = row.GetCell(0).ToString();
                                    smi.PO = row.GetCell(1) == null ? "" : row.GetCell(1).ToString();
                                    smi.PRECIO_SV = (decimal)double.Parse(row.GetCell(2).ToString());
                                    smi.HBI_TIL = (decimal)double.Parse(row.GetCell(3)== null ? "0" : row.GetCell(3).ToString());
                                    //smi.SAFETY_SCTOCK = (decimal)double.Parse(row.GetCell(4).ToString() == null ? "0" : row.GetCell(4).ToString());
                                    //smi.ON_HAND_CORDER = (decimal)double.Parse(row.GetCell(5).ToString() == null ? "0" : row.GetCell(5).ToString());
                                    //smi.ON_HAND_SMI = (decimal)double.Parse(row.GetCell(6).ToString() == null ? "0" : row.GetCell(6).ToString());
                                    //smi.QTY = (decimal)double.Parse(row.GetCell(7).ToString());
                                    //smi.PERCENT_SMI = (decimal)double.Parse(row.GetCell(8).ToString());
                                    //smi.QTY_PROCESS= (decimal)double.Parse(row.GetCell(9).ToString());
                                    //smi.QTY_RED = (decimal)double.Parse(row.GetCell(10).ToString());
                                    smi.DATE_INV_W = row.GetCell(4) == null ? "" : row.GetCell(4).ToString();
                                    smi.VENDOR_COMENTS = row.GetCell(5) == null ? "" : row.GetCell(5).ToString();
                                    smi.ESTADO = row.GetCell(6) == null ? "" : row.GetCell(6).ToString();
                                    db.TER_QW2_HBI.Add(smi);
                                    db.SaveChanges();
                                    flage = true;
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
                    ViewBag.ruta = "QW2";

                    return View("Inicio");
                }
                else
                {
                ViewBag.error = "Archivo con Errores";
                return View("Error");
            }

            


        }


        public JsonResult Validacion()
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec TER_SP_VALIDARQW2");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidacionDR()
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec TER_SP_VALIDAR_DR");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(data: new { success = true, message = "Registros Actualizados" } ,JsonRequestBehavior.AllowGet);
        }
        public JsonResult ValidacionDRMH()
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec TER_SP_VALIDAR_DRMH");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Inventario(string item)
        {
            
            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC TER_TRACE_TE @ITEM";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@ITEM", SqlDbType.VarChar, 30).Value = "%" + item + "%";
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
                STAGE_ID= dataRow.Field<string>("STAGE_ID")
            }).ToList();


            return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InventarioDR(string item)
        {

            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC TER_TRACE_DR @ITEM";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@ITEM", SqlDbType.VarChar, 30).Value = "%" + item + "%";
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
                TRACE_ID = dataRow.Field<string>("TRACE_ID")
            }).ToList();


            return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Procesar(string cantidad, string ITEM,string PO,string Part_id)
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec ACTUALIZAR_INVENTARIO '" + cantidad + "','" + ITEM + "','" + PO + "','" + Part_id + "'");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ProcesarDR(string Part_id, string ID)
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec TER_PART_DR '" + Part_id + "','" + ID + "'");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        //---mh


        public JsonResult ProcesarDRMH(string Part_id, string ID)
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec TER_PART_DR_MH '" + Part_id + "','" + ID + "'");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ListarUMH()
        {
            var formData = HttpContext.Request.Form;
            //var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {
                var update = "yyyy-MM-dd";

                var response = new Editor(db, "TER_QW_MH","ID")
                .Model<TER_QW_MH>()
                .Field(new Field("ID"))
                .Field(new Field("HBI_PART")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                 .Field(new Field("MH_NUMBER")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("STATUS")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("WIP")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("ETA")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("ATDR")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("TRANSITO1")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("TRANSITO2")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                ).Field(new Field("TRANSITO3")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("TRANSITO4")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("CASE_QTY")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("BOXES")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("COMMENTS")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("DAYS")
                .SetFormatter(Format.IfEmpty(null)))
                
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult InventarioDRMH(string item)
        {

            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC TER_TRACE_DR_MH @ITEM";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.Add("@ITEM", SqlDbType.VarChar, 30).Value = "%" + item + "%";
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
                TRACE_ID = dataRow.Field<string>("TRACE_ID")
            }).ToList();


            return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Etiquetas()
        {

            TERMOAPPEntities db = new TERMOAPPEntities();
            var empList = (from p in db.TER_QW2_UNDERWEAR
                             select p).FirstOrDefault();

            return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PreciosSock()
        {
            var formData = HttpContext.Request.Form;
            //var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {
                

                var response = new Editor(db, "TER_PRECIO_SOCK", "ID")
                .Model<TER_PRECIO_SOCK>()
                .Field(new Field("ID"))
                .Field(new Field("PART_ID")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                .Field(new Field("PRECIO1")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRECIO2")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRECIO3")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRECIO4")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRECIO5")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRECIO6")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRECIO7")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRECIO8")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRECIO9")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("PRECIO10")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )


                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }

    }






}