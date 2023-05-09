using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apiweb.Models;
using DataTables;
using System.Web.Http;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Web.Script.Serialization;
using Apiweb.Filtro;
using Apiweb.Properties;
using System.Globalization;

namespace Apiweb.Controllers
{
    [VerificaSesion]
    public class HomeController : Controller
    {
        Settings settings = Properties.Settings.Default;
        Boolean PedidoDominicana = false;
        Boolean PreciosSMI = false;
        Boolean PreciosMH = false;
        Boolean Historico = false;
        Boolean TexportSocks = false;
        Boolean ColocacionOC = false;
        Boolean ColocacionPedido = false;
        Boolean CortosProduccion = false;
        // Importar Excel a la base de datos
        [System.Web.Http.HttpPost]
        public ActionResult DataImportAll(HttpPostedFileBase filebase)
        {
            string valor;
            bool flage = false;
            try { 
           
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
                    TER_QW3_PED smi = new TER_QW3_PED();
                    if (row != null)
                    {
                        using (TERMOAPPEntities db = new TERMOAPPEntities())
                        {

                            if (row.GetCell(0) != null)
                            {
                                valor = row.GetCell(1).ToString();
                                for (int j = 0; 4>valor.Length; j++) 
                                {
                                    valor = "0"+valor ;
                                }

                                smi.PO_NUMBER = row.GetCell(0).ToString().Replace(" ", String.Empty) +"-"+ valor;
                                smi.LINEA = Int32.Parse(row.GetCell(2).ToString().Replace(" ", String.Empty));
                                smi.ITEM = row.GetCell(3) == null ? "0" : row.GetCell(3).ToString().Replace(" ", String.Empty); 
                                smi.QUANTITY = (decimal)double.Parse(row.GetCell(4) == null ? "0" : row.GetCell(4).ToString().Replace(" ", String.Empty));
                                smi.PRECIO_PO = (decimal)double.Parse(row.GetCell(5)== null ? "0" : row.GetCell(5).ToString().Replace(" ", String.Empty));
                                smi.ESTADO = "A";
                                db.TER_QW3_PED.Add(smi);
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
                    ViewBag.ruta = "Home";

                    return View("Inicio");
}
                else
{
                ViewBag.error = "Archivo con Errores";
                return View("Error");
            }



        }







        // Importar Excel a la base de datos
        [System.Web.Http.HttpPost]
        public ActionResult DataImportAllSMI(HttpPostedFileBase filebase)
        {
            string valor;
            bool flage = false;
            try { 
            HttpPostedFileBase file = Request.Files["FileUpload"]; // Obtener archivos cargados


            if (file == null || file.ContentLength < 0 || file.FileName == "")
            {
                    //ViewBag.error = "El archivo no puede estar vacío";
                    return View("SMI");
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
                    TER_QW3_PRECIO_SMI smi = new TER_QW3_PRECIO_SMI();
                    if (row != null)
                    {
                        using (TERMOAPPEntities db = new TERMOAPPEntities())
                        {

                            if (row.GetCell(0) != null)
                            {
                                valor = row.GetCell(1).ToString();
                                for (int j = 0; 4 > valor.Length; j++)
                                {
                                    valor = "0" + valor;
                                }

                                smi.HBI_NUMBER = row.GetCell(0).ToString() ;
                                smi.PRECIO = (decimal)double.Parse(row.GetCell(1).ToString());
                                db.TER_QW3_PRECIO_SMI.Add(smi);
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
                    ViewBag.link = "PrecioSMI";
                    ViewBag.ruta = "Home";

                    return View("Inicio");
}
                else
{
                ViewBag.error = "Archivo con Errores";
                return View("Error");
            }


        }


        public ActionResult DataImportAllMH(HttpPostedFileBase filebase)
        {
            string valor;
            bool flage = false;
            try {
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
                    TER_QW3_PRECIO_MH smi = new TER_QW3_PRECIO_MH();
                    if (row != null)
                    {
                        using (TERMOAPPEntities db = new TERMOAPPEntities())
                        {

                            if (row.GetCell(0) != null)
                            {
                                valor = row.GetCell(1).ToString();
                                for (int j = 0; 4 > valor.Length; j++)
                                {
                                    valor = "0" + valor;
                                }

                                smi.MH_NUMBER = row.GetCell(0) == null ? "" : row.GetCell(0).ToString();
                                smi.MHDATE = row.GetCell(1)== null ? "" : Convert.ToDateTime(row.GetCell(1).ToString()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); ; 
                                smi.LINE= Int32.Parse(row.GetCell(2).ToString());
                                smi.RELEASE_NUMBER= row.GetCell(3)== null || row.GetCell(3).ToString().Equals("") ? "" : row.GetCell(3).ToString();
                                smi.ITEM= row.GetCell(4).ToString();
                                smi.PRECIO= (decimal)double.Parse(row.GetCell(5).ToString());
                                smi.REQUES_QTY= (decimal)double.Parse(row.GetCell(6).ToString());
                                smi.PRODUC_QTY = row.GetCell(7) == null || row.GetCell(7).ToString().Equals("") ? 0 : (decimal)double.Parse(row.GetCell(7).ToString());
                                smi.PEDIDO_TERMO= row.GetCell(8) == null || row.GetCell(8).ToString().Equals("") ? "" : row.GetCell(8).ToString();
                                smi.COMENTARIOS= row.GetCell(9) == null || row.GetCell(9).ToString().Equals("") ? "": row.GetCell(9).ToString();
                                db.TER_QW3_PRECIO_MH.Add(smi);
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
                ViewBag.link = "PrecioMH";
                ViewBag.ruta = "Home";

                return View("Inicio");
            }
            else
            {
                ViewBag.error = "Archivo con Errores";
                return View("Error");
            }



        }


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

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ValidarRol();
            if (PedidoDominicana) 
            { return View(); }
            else
            { return View("Error"); }
            
        }
        public ActionResult Inicio()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult PrecioSMI()
        {
            ViewBag.Title = "Home Page";
            ValidarRol();
            if (PreciosSMI)
            { return View(); }
            else
            { return View("Error"); }
        }

        public ActionResult PrecioMH()
        {
            ViewBag.Title = "Home Page";
            ValidarRol();
            if (PreciosMH)
            { return View(); }
            else
            { return View("Error"); }
        }
        public ActionResult Historicos()
        {
            ViewBag.Title = "Home Page";
            ValidarRol();
            if (Historico)
            { return View(); }
            else
            { return View("Error"); }
        }
        public ActionResult PrecioMHV()
        {
            var update = "dd/MM/yyyy";
            
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {

                var response = new Editor(db, "TER_QW3_PRECIO_MH", "ID")
                .Model<TER_QW3_PRECIO_MH>()
                .Field(new Field("MH_NUMBER")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                .Field(new Field("MHDATE"))
                .Field(new Field("LINE"))
                .Field(new Field("RELEASE_NUMBER"))
                .Field(new Field("ITEM"))
                .Field(new Field("PRECIO"))
                .Field(new Field("REQUES_QTY"))
                .Field(new Field("PRODUC_QTY"))
                .Field(new Field("PEDIDO_TERMO"))
                
                .Field(new Field("COMENTARIOS"))

                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }
        }



        public ActionResult PrecioSMIv()
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {

                var response = new Editor(db, "TER_QW3_PRECIO_SMI", "ID")
                .Model<TER_QW3_PRECIO_SMI>()
                .Field(new Field("HBI_NUMBER")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))

                .Field(new Field("PRECIO")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )

                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }
        }
        [ValidateInput(false)]
        public ActionResult Listar()
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;
            try
            {
                using (var db = new Database("sqlserver", settings.DBConnection))
                {

                    var response = new Editor(db, "TER_QW3_PED", "ID").Where("ESTADO", "A")
                    .Model<TER_QW3_PED>()
                    .Field(new Field("PO_NUMBER")
                    .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                    .Field(new Field("LINEA"))
                    .Field(new Field("ITEM"))
                    .Field(new Field("QUANTITY")
                    .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                    .SetFormatter(Format.IfEmpty(null))
                    )
                    .Field(new Field("PRECIO_PO")
                    .Validator(Validation.Numeric())
                    .SetFormatter(Format.IfEmpty(null)))
                    .Field(new Field("PRECIO_OK")
                    .Validator(Validation.Numeric())
                    .SetFormatter(Format.IfEmpty(null)))
                    .Field(new Field("VALIDACION"))
                    .Field(new Field("ON_HAND")
                    .Validator(Validation.Numeric())
                    .SetFormatter(Format.IfEmpty(null)))
                    .Field(new Field("CANTIDAD_X_CAJA")
                    .Validator(Validation.Numeric())
                    .SetFormatter(Format.IfEmpty(null)))
                    .Field(new Field("CAJA_CERRADA")
                    .Validator(Validation.Numeric())
                    .SetFormatter(Format.IfEmpty(null)))
                    .Field(new Field("DESPACHADO")
                    .Validator(Validation.Numeric())
                    .SetFormatter(Format.IfEmpty(null)))
                    .Field(new Field("CANTIDAD_ENVIAR")
                    .Validator(Validation.Numeric())
                    .SetFormatter(Format.IfEmpty(null)))
                    .Field(new Field("CAJA_DESPACHAR")
                    .SetFormatter(Format.IfEmpty(null)))
                    .Field(new Field("DIRECCION")
                    .SetFormatter(Format.IfEmpty(null)))
                    .Field(new Field("COMENTARIO")
                    .SetFormatter(Format.IfEmpty(null)))
                    .Field(new Field("ORDEN")
                    .SetFormatter(Format.IfEmpty(null)))
                    .Process(formData)
                    .Data();
                    var A = Response;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e) 
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
       

          

               
               

            

        }


        public JsonResult AutoComplete()
        {
            Array array;
            List<String> intermediate_list = new List<String>();
            List<TER_QW3_PED> customers = new List<TER_QW3_PED>();
            TERMOAPPEntities db = new TERMOAPPEntities();
            customers = db.TER_QW3_PED.ToList();

           
            foreach (var s in customers) 
            {
                intermediate_list.Add(s.PO_NUMBER);
            }
            array = intermediate_list.ToArray();
            return Json(array, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Validacion()
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec TER_SP_VALIDAR");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Inventario(string item, string po)
        {

            string[] PO = po.Split(new[] { "-" }, StringSplitOptions.None);
            SqlConnection con = null;
            DataSet ds = null;
            List<Inventarios> custlist = null;
            try
            {
                con = new SqlConnection(settings.DBConnection);
                SqlCommand cmd = new SqlCommand("TER_TRACE_PO", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ITEM", "%" + item.Replace(" ", String.Empty) + "%");
                cmd.Parameters.AddWithValue("@PO", PO[0].Replace(" ", String.Empty) + "%");
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                ds = new DataSet();
                da.Fill(ds);
                custlist = new List<Inventarios>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Inventarios cobj = new Inventarios();
                    cobj.PART_ID = ds.Tables[0].Rows[i]["PART_ID"].ToString();
                    cobj.ITEM = ds.Tables[0].Rows[i]["USER_10"].ToString();
                    cobj.INVENTARIO = Convert.ToDecimal(ds.Tables[0].Rows[i]["INVENTARIO"].ToString());
                    cobj.DESCRIPCION = ds.Tables[0].Rows[i]["APROPERTY_1"].ToString();
                    cobj.FECHA = ds.Tables[0].Rows[i]["APROPERTY_3"].ToString();
                    cobj.CAJA = ds.Tables[0].Rows[i]["CAJA"].ToString();
                    cobj.TRACE_ID = ds.Tables[0].Rows[i]["TRACE_ID"].ToString();
                    cobj.CASE_QTY = ds.Tables[0].Rows[i]["CASE_QTY"].ToString();
                    custlist.Add(cobj);
                }

                return Json(new { data = custlist }, JsonRequestBehavior.AllowGet);
         
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { data = custlist }, JsonRequestBehavior.AllowGet);
              
            }
            finally
            {
                con.Close();
            }
         
        }


        public JsonResult Procesar(string Part_id ,string ITEM)
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"exec TER_Cambiar_part '" + Part_id+"','"+ ITEM + "'");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Pedido()
        {
            bool respuesta = true;
            try
            {
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {


                    var data = db.Database.ExecuteSqlCommand(@"EXEC  [dbo].[TER_SP_PEDIDOS]");



                }
            }
            catch
            {
                respuesta = false;
            }



            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Pedido1()
        {
            
            TERMOAPPEntities db = new TERMOAPPEntities();
            List<orderDetail> detalle = new List<orderDetail>();
            order orden = new order()
            {
                inv = "",
                head_status = true,
                baseNombre = "TERMO9",
                order_Id = "21/16143-PED",
                CUSTOMER_ID = "SV-TEXPOR",
                CONTACT_FIRST_NAME = "JEANNELLY",
                CONTACT_LAST_NAME = "PEÑA",
                CONTACT_HONORIFIC = "Miss",
                CONTACT_SALUDATION = "Dear Miss PEÑA:",
                CONTACT_PHONE = " ",
                CONTACT_FAX = " ",
                SHIP_TO_ADDR_NO = "2",
                TERRITORY = "",
                SALESREP_ID = "V-EDUOLM",
                SITE_ID = "TERCA",
                TERMS_NET_TYPE = "A",
                TERMS_NET_DAYS = "120",
                TERMS_DISC_TYPE = "A",
                TERMS_DESCRIPTION = "0",
                ORDER_DATE = DateTime.Now.ToString("yyyy-MM-dd"),
                DESIRED_SHIP_DATE = DateTime.Now.ToString("yyyy-MM-dd"),
                LAST_SHIPPED_DATE = " ",
                TOTAL_AMT_ORDERED = "1975.10000000",
                TOTAL_AMT_SHIPPED = "1975.10000000",
                PROMISE_DATE = " ",
                //EDI_BLANKET_FLAG = " ",
                SHIPTO_ID = "2",
                CURRENCY_ID = "USD",
                CARRIER_ID = " ",
                CONTACT_MOBILE = " ",
                CREATE_DATE = DateTime.Now.ToString("yyyy-MM-dd"),
                USER_5 = " ",
                //USER_8 = " ",
                USER_9 = " ",
                STATUS_EFF_DATE = " ",
                TERMS_ID = "120 DIAS",
                CONTACT_ID = "0000004",
                ENTERED_BY = "GORREGO",
                oreder_detail = new List<orderDetail>()
                   { new orderDetail{
                    LINE_NO=1,
                    PART_ID = "TESI-HANBRA-10.25X12.25-AP5257",
        ORDER_QTY = "10.00000000",
        USER_ORDER_QTY = "10.00000000",
        SELLING_UM = "MI",
        UNIT_PRICE = "197.51000000",
        MISC_REFERENCE = "BLS in 10.25X12.25+1FFx4gg AP5257",
        PRODUCT_CODE = "PT-EF-TEX",
        COMMODITY_CODE = "EI00-TX-Z0-LDP",
        GL_REVENUE_ACCT_ID = "5010203-50-01",
        LAST_SHIPPED_DATE = " ",
        TOTAL_SHIPPED_QTY = " ",
        TOTAL_USR_SHIP_QTY = " ",
        TOTAL_AMT_ORDERED = " ",
        TOTAL_AMT_SHIPPED = " ",
        SERVICE_CHARGE_ID = " ",
        VAT_CODE = " ",
        WAREHOUSE_ID = " ",
        //WIP_VAS_UNIT_PRICE = " ",
        //ACCEPTED_EARLY = " ",
        //DAYS_EARLY = " ",
        USER_1 = " ",
        ORIGIN_STAGE_REVISION_ID = " ",
        STATUS_EFF_DATE = " ",
        ENTERED_BY = "GORREGO"} }




            };
            var result="";
            var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://termovm9:8091/api/order/Post");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(orden);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                 result = streamReader.ReadToEnd();
            }
            Console.Out.WriteLine(result);
            return Json(result, JsonRequestBehavior.AllowGet);
        }





        public ActionResult Listar2()
        {
            var update = "dd/MM/yyyy";

            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {

                var response = new Editor(db, "TER_QW3_PED_HIST", "ID")
                .Model<TER_QW3_PED_HIST>()
                .Field(new Field("ID"))
                .Field(new Field("PO_NUMBER"))
                .Field(new Field("LINEA"))
                .Field(new Field("ITEM"))
                .Field(new Field("QUANTITY"))
                .Field(new Field("PRECIO_PO"))
                .Field(new Field("PRECIO_OK"))
                .Field(new Field("VALIDACION"))
                .Field(new Field("ON_HAND"))
                .Field(new Field("CANTIDAD_X_CAJA"))
                .Field(new Field("CAJA_CERRADA"))
                .Field(new Field("DESPACHADO"))
                .Field(new Field("CANTIDAD_ENVIAR"))
                .Field(new Field("COMENTARIO"))
                .Field(new Field("ESTADO"))
                .Field(new Field("PART_ID"))
                .Field(new Field("CAJA_DESPACHAR"))
                .Field(new Field("CLIENTE"))
                .Field(new Field("PEDIDO"))
                .Field(new Field("PED_LINE"))
                .Field(new Field("FECHA"))
                .Process(formData)
                .Data();

                
                var json = Json(response, JsonRequestBehavior.AllowGet);

                json.MaxJsonLength = 500000000;
                return json;

            }
        }


    }
}
