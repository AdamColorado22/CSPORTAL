using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Apiweb.App_Start;
using Apiweb.Filtro;
using Apiweb.Models;
using Apiweb.Properties;
using Apiweb.Utilidad;
using DataTables;
using log4net;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
namespace Apiweb.Controllers
{
    [VerificarSesion]
    public class WorkFlowController : Controller
    {
        private static readonly ILog Log = Logs.GetLogger();
        AppDbContext _dbContext;
        Correo correos = new Correo();
        // GET: WorkFlow
        Settings settings = Properties.Settings.Default;
        string usuario = SessionManager.GetUsuario();
        string entidad = SessionManager.GetEntidad();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Muestras()
        {
            return View();
        }

        public ActionResult CreacionArt()
        {
            return View();
        }

        public ActionResult ServiciosProd()
        {
            return View();
        }

        public ActionResult Artes()
        {
            return View();
        }

        public ActionResult Otros()
        {
            return View();
        }


        public JsonResult ListarB2BJ()
        {
            

            Log.Info("Listando Workflow");
            try { 
            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC TER_WORKFLOWB2B_LISTAR '" + usuario + "' ";
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
            .Select(dataRow => new WORKFLOWB2B
            {
                ID = dataRow.Field<Int32>("ID"),
                TITULO = dataRow.Field<string>("TITULO"),
                TIPO_SERVICIO = dataRow.Field<string>("TIPO_SERVICIO"),
                IDCLIENTE = dataRow.Field<string>("IDCLIENTE"),
                PASO_ACTUAL = dataRow.Field<Int32>("PASO_ACTUAL"),
                DUI = dataRow.Field<string>("DUI"),
                COTIZACION = dataRow.Field<string>("REFERENCIA_COSTEO"),
                CUST_PART_ID = dataRow.Field<string>("CUSTOMER_PART_ID"),
                COMMODITY_CODE = dataRow.Field<string>("COMMODITY_CODE"),
                PROCESO_ACTUAL = dataRow.Field<string>("PROCESO_ACTUAL"),
                FECHA_INICIO = dataRow.Field<string>("FECHA_INICIO"),
                TIEMPO_PROCESO = dataRow.Field<Decimal>("TIEMPO_PROCESO"),
                TIEMPO_TOTAL = dataRow.Field<Decimal>("TIEMPO_TOTAL"),
                FECHA_FINALIZACION = dataRow.Field<string>("FECHA_FINALIZACION"),
                DIAS_TOTALES= dataRow.Field<Int32>("DIAS_TOTALES"),
                FECHA_ESTIMADA= dataRow.Field<string>("FECHA_ESTIMADA"),
                USUARIO_INI = dataRow.Field<string>("USUARIO_INI")
            }).ToList();
                return Json(new { data = Data }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
               {
                Log.Error(String.Format("Error al Consultar", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);

            }
                
       

        }
        public JsonResult ListarCrea()
        {


            Log.Info("Listando Workflow ListarCrea");

            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC TER_WORKFLOWART_LISTAR '" + usuario + "' ";
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
            .Select(dataRow => new TER_W_WORFLOW_CARTICULO
            {
                ID = dataRow.Field<Int32>("ID"),
                DUI = dataRow.Field<string>("DUI"),
                TITULO = dataRow.Field<string>("TITULO"),
                NUM_PEDIDO = dataRow.Field<string>("NUM_PEDIDO"),
                VALIDACION = dataRow.Field<string>("VALIDACION"),
                CARTILLA_CALIDAD = dataRow.Field<string>("CARTILLA_CALIDAD"),
                NUM_CARTILLA = dataRow.Field<string>("NUM_CARTILLA"),
                TIPO_PEDIDO = dataRow.Field<string>("TIPO_PEDIDO"),
                FECHA_ENTREGA = dataRow.Field<string>("FECHA_ENTREGA"),
                FECHA_VALIDACION_IMP = dataRow.Field<string>("FECHA_VALIDACION_IMP"),
                PASO_ACTUAL = dataRow.Field<Int32>("PASO_ACTUAL"),
                FECHA_INICIO = dataRow.Field<string>("FECHA_INICIO"),
                TIEMPO_PROCESO = dataRow.Field<Decimal>("TIEMPO_PROCESO"),
                TIEMPO_TOTAL = dataRow.Field<Decimal>("TIEMPO_TOTAL"),
                FECHA_FINALIZACION = dataRow.Field<string>("FECHA_FINALIZACION"),
                DIAS_TOTALES = dataRow.Field<Int32>("DIAS_TOTALES"),
                FECHA_ESTIMADA = dataRow.Field<string>("FECHA_ESTIMADA"),
                PROCESO_ACTUAL= dataRow.Field<string>("PROCESO_ACTUAL")
            }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ListaMuestras()
        {


            Log.Info("Listando Workflow ListaMuestras");

            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC [TER_WORKFLOWMUE_LISTAR] '" + usuario + "' ";
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
            .Select(dataRow => new Muestras
            {
                ID = dataRow.Field<Int32>("ID"),
                PRODUCTO = dataRow.Field<string>("PRODUCTO"),
                DUI = dataRow.Field<string>("DUI"),
                IDCLIENTE = dataRow.Field<string>("IDCLIENTE"),
                NOMBRE_PRODUCTO = dataRow.Field<string>("NOMBRE_PRODUCTO"),
                TIPO_PROYECTO = dataRow.Field<string>("TIPO_PROYECTO"),
                FECHA_REQUERIDA = dataRow.Field<string>("FECHA_REQUERIDA"),
                MUESTRA = dataRow.Field<string>("MUESTRA"),
                ESTRUCTURA = dataRow.Field<string>("ESTRUCTURA"),
                CODIGO_U_MUESTRA = dataRow.Field<string>("CODIGO_U_MUESTRA"),
                PART_ID = dataRow.Field<string>("PART_ID"),
                DUI_MUESTRA = dataRow.Field<string>("DUI_MUESTRA"),
                MAESTR_ING = dataRow.Field<string>("MAESTR_ING"),
                NUM_PEDIDO = dataRow.Field<string>("NUM_PEDIDO"),
                CANTIDAD = dataRow.Field<string>("CANTIDAD"),
                FORMULA = dataRow.Field<string>("FORMULA"),
                P_EQUIPO_TEC = dataRow.Field<string>("P_EQUIPO_TEC"),
                EXP_MATERIALES = dataRow.Field<string>("EXP_MATERIALES"),
                OT = dataRow.Field<string>("OT"),
                FECHA_EST_ENTRAGA = dataRow.Field<string>("FECHA_EST_ENTRAGA"),
                TP_CANTIDAD = dataRow.Field<string>("TP_CANTIDAD"),
                PROCESO_ACTUAL = dataRow.Field<string>("PROCESO_ACTUAL"),
                FECHA_INICIO = dataRow.Field<string>("FECHA_INICIO"),
                TIEMPO_PROCESO = dataRow.Field<decimal>("TIEMPO_PROCESO"),
                TIEMPO_TOTAL = dataRow.Field<decimal>("TIEMPO_TOTAL"),
                FECHA_ESTIMADA = dataRow.Field<string>("FECHA_ESTIMADA"),
                FECHA_FINALIZACION = dataRow.Field<string>("FECHA_FINALIZACION"),
                DIAS_TOTALES = dataRow.Field<Int32>("DIAS_TOTALES"),
                PASO_ACTUAL = dataRow.Field<Int32>("PASO_ACTUAL")

            }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ListaServiciosProd()
        {


            Log.Info("Listando Workflow ListaServiciosProd");

            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC [TER_WORKFLOWPROD_LISTAR]'" + usuario + "' ";
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
            .Select(dataRow => new Produccion
            {
                ID = dataRow.Field<Int32>("ID"),
                TIPO = dataRow.Field<string>("TIPO"),
                TIPO_REQUERIMIENTO = dataRow.Field<string>("TIPO_REQUERIMIENTO"),
                DESCRIPCION = dataRow.Field<string>("DESCRIPCION"),
                IDCLIENTE = dataRow.Field<string>("IDCLIENTE"),
                DUI = dataRow.Field<string>("DUI"),
                FECHA_ESTIMADA = dataRow.Field<string>("FECHA_ESTIMADA"),
                PERSONA_ASIGNADA = dataRow.Field<string>("PERSONA_ASIGNADA"),
                TRABAJO_REALIZADO = dataRow.Field<string>("TRABAJO_REALIZADO"),
                COMENTARIOS = dataRow.Field<string>("COMENTARIOS"),
                PROCESO_ACTUAL = dataRow.Field<string>("PROCESO_ACTUAL"),
                FECHA_INICIO = dataRow.Field<string>("FECHA_INICIO"),
                TIEMPO_PROCESO = dataRow.Field<decimal>("TIEMPO_PROCESO"),
                TIEMPO_TOTAL = dataRow.Field<decimal>("TIEMPO_TOTAL"),
                FECHA_FINALIZACION = dataRow.Field<string>("FECHA_FINALIZACION"),
                DIAS_TOTALES = dataRow.Field<Int32>("DIAS_TOTALES"),
                PASO_ACTUAL = dataRow.Field<Int32>("PASO_ACTUAL"),


            }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);

        }



        public JsonResult ListaServiciosOtros()
        {


            Log.Info("Listando Workflow ListaServiciosProd");

            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC [TER_WORKFLOWOTROS_LISTAR]'" + usuario + "' ";
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
            .Select(dataRow => new Otros
            {
                ID = dataRow.Field<Int32>("ID"),
                TIPO = dataRow.Field<string>("TIPO"),
                TIPO_SERVICIO = dataRow.Field<string>("TIPO_SERVICIO"),
                IDCLIENTE = dataRow.Field<string>("IDCLIENTE"),
                DUI = dataRow.Field<string>("DUI"),
                CANTIDA_MAT_C = dataRow.Field<string>("CANTIDA_MAT_C"),
                CANTIDAD_DUMMIES = dataRow.Field<string>("CANTIDAD_DUMMIES"),
                ESPECIFICACIONES = dataRow.Field<string>("ESPECIFICACIONES"),
                ASIGNADA = dataRow.Field<string>("ASIGNADA"),
                PASO_ACTUAL = dataRow.Field<Int32>("PASO_ACTUAL"),
                PROCESO_ACTUAL = dataRow.Field<string>("PROCESO_ACTUAL"),
                FECHA_INICIO = dataRow.Field<string>("FECHA_INICIO"),
                TIEMPO_PROCESO = dataRow.Field<decimal>("TIEMPO_PROCESO"),
                TIEMPO_TOTAL = dataRow.Field<decimal>("TIEMPO_TOTAL"),
                FECHA_ESTIMADA = dataRow.Field<string>("FECHA_ESTIMADA"),
                FECHA_FINALIZACION = dataRow.Field<string>("FECHA_FINALIZACION"),
                DIAS_TOTALES = dataRow.Field<Int32>("DIAS_TOTALES"),
                



            }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);

        }




        public JsonResult ListaServiciosSar()
        {


            Log.Info("Listando Workflow ListaServiciosProd");

            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC [TER_WORKFLOWSAR_LISTAR]'" + usuario + "' ";
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
            .Select(dataRow => new SAR
            {
                ID = dataRow.Field<Int32>("ID"),
                TIPO = dataRow.Field<string>("TIPO"),
                ARTE = dataRow.Field<string>("ARTE"),
                DUI = dataRow.Field<string>("DUI"),
                RETOQUE = dataRow.Field<string>("RETOQUE"),
                IMPRESION = dataRow.Field<string>("IMPRESION"),
                ASIGNADO = dataRow.Field<string>("ASIGNADO"),
                PASO_ACTUAL = dataRow.Field<Int32>("PASO_ACTUAL"),
                FECHA_INICIO = dataRow.Field<string>("FECHA_INICIO"),
                TIEMPO_PROCESO = dataRow.Field<decimal>("TIEMPO_PROCESO"),
                TIEMPO_TOTAL = dataRow.Field<decimal>("TIEMPO_TOTAL"),
                FECHA_ESTIMADA = dataRow.Field<string>("FECHA_ESTIMADA"),
                FECHA_FINALIZACION = dataRow.Field<string>("FECHA_FINALIZACION"),
                DIAS_TOTALES = dataRow.Field<Int32>("DIAS_TOTALES"),
                PROCESO_ACTUAL = dataRow.Field<string>("PROCESO_ACTUAL"),
                NOMBRE_PRODUCTO= dataRow.Field<string>("NOMBRE_PRODUCTO"),
                USUARIO_INICIADOR = dataRow.Field<string>("USUARIO_INICIADOR"),
                REGION = dataRow.Field<string>("REGION"),
                CLIENTE = dataRow.Field<string>("CLIENTE")


            }).ToList();

            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Guardar(WORKFLOWB2B objeto)
        {
            try
            {

              string sql = @" EXEC TER_WOKFLOWB2B_INICIO @ID,@TIPO,@TITULO,@PASO_ACTUAL,@TIPO_SERVICIO,@IDCLIENTE,@NOTAS,@USUARIO,@ARCHIVO,@FECHA_RECEPCION";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@TIPO", "B2B"),
                        new SqlParameter("@TITULO", String.IsNullOrEmpty(objeto.TITULO) ? "" : objeto.TITULO),
                        new SqlParameter("@PASO_ACTUAL", objeto.PASO_ACTUAL),
                        new SqlParameter("@TIPO_SERVICIO", String.IsNullOrEmpty(objeto.TIPO_SERVICIO) ? "" : objeto.TIPO_SERVICIO),
                        new SqlParameter("@IDCLIENTE", String.IsNullOrEmpty(objeto.IDCLIENTE) ? "" : objeto.IDCLIENTE),
                        new SqlParameter("@NOTAS", String.IsNullOrEmpty(objeto.NOTAS) ? "" : objeto.NOTAS),
                        new SqlParameter("@USUARIO", String.IsNullOrEmpty(usuario) ? "" : usuario),
                        new SqlParameter("@ARCHIVO", "NO"),
                        new SqlParameter("@FECHA_RECEPCION", objeto.FECHA)
                        );
                        db.SaveChanges();
                        dbContextTransaction.Commit();

                    }
                     }
                DateTime now = DateTime.Now;
                envioCorreo(objeto);
                return Json(data: new { success = true, message = "Datos Ingresados" }, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Insertar", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GuardarArt(TER_W_WORFLOW_CARTICULO objeto)
        {
            try
            {

                string sql = @" EXEC TER_SP_W_CREACION_ART @ID,@DUI,@IDCLIENTE,@SAR,@TIPO_PROYECTO,@OCCANTIDAD";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@DUI", objeto.DUI),
                        new SqlParameter("@IDCLIENTE", objeto.IDCLIENTE),
                        new SqlParameter("@SAR", objeto.SAR),
                        new SqlParameter("@TIPO_PROYECTO", objeto.TIPO_PROYECTO),
                        new SqlParameter("@OCCANTIDAD", objeto.OC_CANTIDAD_SOLICITADA));
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



        public JsonResult GuardarMue(Muestras objeto)
        {
            try
            {

                string sql = @" EXEC TER_SP_W_CREACION_MUE @ID,@DUI,@IDCLIENTE,@NOMBRE_PRODUCTO,@TIPO_PROYECTO,@FECHA_REQUERIDA,@CANT_MUESTRA_REQ,@TIPO_IMPRESION,@EMPAQUE_ALIMENTOS";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
    new SqlParameter("@ID", objeto.ID),
    new SqlParameter("@DUI", String.IsNullOrEmpty(objeto.DUI) ? "" : objeto.DUI),
    new SqlParameter("@IDCLIENTE", String.IsNullOrEmpty(objeto.IDCLIENTE) ? "" : objeto.IDCLIENTE),
    new SqlParameter("@NOMBRE_PRODUCTO", String.IsNullOrEmpty(objeto.NOMBRE_PRODUCTO) ? "" : objeto.NOMBRE_PRODUCTO),
    new SqlParameter("@TIPO_PROYECTO", String.IsNullOrEmpty(objeto.TIPO_PROYECTO) ? "" : objeto.TIPO_PROYECTO),
    new SqlParameter("@FECHA_REQUERIDA", String.IsNullOrEmpty(objeto.FECHA_REQUERIDA) ? "" : objeto.FECHA_REQUERIDA),
    new SqlParameter("@CANT_MUESTRA_REQ",objeto.CANT_MUESTRA_REQ ),
    new SqlParameter("@TIPO_IMPRESION", String.IsNullOrEmpty(objeto.TIPO_IMPRESION) ? "" : objeto.TIPO_IMPRESION),
    new SqlParameter("@EMPAQUE_ALIMENTOS", objeto.EMPAQUE_ALIMENTOS ? "1" : "0")

);

                        db.SaveChanges();
                        dbContextTransaction.Commit();


                    }
                }
                DateTime now = DateTime.Now;
                envioCorreo3(objeto);
                return Json(data: new { success = true, message = "Datos Ingresados" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Insertar", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GuardarProd(Produccion objeto)
        {
            try
            {

                string sql = @" EXEC [TER_SP_W_CREACION_PROD] @ID,@TIPO_REQUERIMIENTO,@DESCRIPCION,@IDCLIENTE,@DUI,@PERSONA_ASIGNADA,@FECHA_REQUERIDA";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@TIPO_REQUERIMIENTO", String.IsNullOrEmpty(objeto.TIPO_REQUERIMIENTO) ? "" : objeto.TIPO_REQUERIMIENTO),
                        new SqlParameter("@DESCRIPCION", String.IsNullOrEmpty(objeto.DESCRIPCION) ? "" : objeto.DESCRIPCION),
                         new SqlParameter("@IDCLIENTE", String.IsNullOrEmpty(objeto.IDCLIENTE) ? "" : objeto.IDCLIENTE),
                          new SqlParameter("@DUI", String.IsNullOrEmpty(objeto.DUI) ? "" : objeto.DUI),
                           new SqlParameter("@PERSONA_ASIGNADA", String.IsNullOrEmpty(objeto.PERSONA_ASIGNADA) ? "" : objeto.PERSONA_ASIGNADA),
                           new SqlParameter("@FECHA_REQUERIDA", String.IsNullOrEmpty(objeto.FECHA_REQUERIDA) ? "" : objeto.FECHA_REQUERIDA));
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


        public JsonResult GuardarOtros(Otros objeto)
        {
            try
            {

                string sql = @" EXEC [TER_SP_W_CREACION_OTROS] @ID,@TIPO_SERVICIO,@IDCLIENTE,@DUI,@CANTIDA_MAT_C,@CANTIDAD_DUMMIES,@ESPECIFICACIONES,@ASIGNADA";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@TIPO_SERVICIO", String.IsNullOrEmpty(objeto.TIPO_SERVICIO) ? "" : objeto.TIPO_SERVICIO),
                        new SqlParameter("@IDCLIENTE", String.IsNullOrEmpty(objeto.IDCLIENTE) ? "" : objeto.IDCLIENTE),
                         new SqlParameter("@DUI", String.IsNullOrEmpty(objeto.DUI) ? "" : objeto.DUI),
                          new SqlParameter("@CANTIDA_MAT_C", String.IsNullOrEmpty(objeto.CANTIDA_MAT_C) ? "" : objeto.CANTIDA_MAT_C),
                           new SqlParameter("@CANTIDAD_DUMMIES", String.IsNullOrEmpty(objeto.CANTIDAD_DUMMIES) ? "" : objeto.CANTIDAD_DUMMIES),
                           new SqlParameter("@ESPECIFICACIONES", String.IsNullOrEmpty(objeto.ESPECIFICACIONES) ? "" : objeto.ESPECIFICACIONES),
                           new SqlParameter("@ASIGNADA", String.IsNullOrEmpty(objeto.ASIGNADA) ? "" : objeto.ASIGNADA));
                        db.SaveChanges();
                        dbContextTransaction.Commit();

                    }
                }
                DateTime now = DateTime.Now;
                envioCorreo6(objeto);
                return Json(data: new { success = true, message = "Datos Ingresados" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Insertar", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GuardarSar(SAR objeto)
        {
            try
            {

                string sql = @" EXEC [TER_SP_W_CREACION_SAR] @ID,@ARTE,@DUI,@RETOQUE,@IMPRESION,@REGION,@NOMBRE_PRODUCTO,@CLIENTE,@USUARIO";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@ARTE", String.IsNullOrEmpty(objeto.ARTE) ? "" : objeto.ARTE),
                        new SqlParameter("@DUI", String.IsNullOrEmpty(objeto.DUI) ? "" : objeto.DUI),
                         new SqlParameter("@RETOQUE", String.IsNullOrEmpty(objeto.RETOQUE) ? "" : objeto.RETOQUE),
                          new SqlParameter("@IMPRESION", String.IsNullOrEmpty(objeto.IMPRESION) ? "" : objeto.IMPRESION),
                        new SqlParameter("@REGION", String.IsNullOrEmpty(objeto.REGION) ? "" : objeto.REGION),
                        new SqlParameter("@NOMBRE_PRODUCTO", String.IsNullOrEmpty(objeto.NOMBRE_PRODUCTO) ? "" : objeto.NOMBRE_PRODUCTO),
                        new SqlParameter("@CLIENTE", String.IsNullOrEmpty(objeto.CLIENTE) ? "" : objeto.CLIENTE),
                        new SqlParameter("@USUARIO", SessionManager.GetUsuario()));
                        db.SaveChanges();
                        dbContextTransaction.Commit();

                    }
                }
                DateTime now = DateTime.Now;
                envioCorreo5(objeto);
                return Json(data: new { success = true, message = "Datos Ingresados" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Insertar", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Siguiente(WORKFLOWB2B objeto)
        {    
            try
            {
                string sql2 = "SELECT ''";
                switch (objeto.PASO_ACTUAL)
                {
                    case 20:
                        sql2 = String.Format("UPDATE TER_W_WORKFLOWB2B SET DUI='{0}' WHERE ID={1} ", objeto.DUI, objeto.ID);
                        break;
                    case 30:
                        sql2 = String.Format("UPDATE TER_W_WORKFLOWB2B SET REFERENCIA_COSTEO='{0}', CELULA_TECNICA='{1}', INFORMACION_REQ='{2}',NECESITA_MUESTRA='{3}'  WHERE ID={4} ", objeto.COTIZACION, objeto.CELULA_TECNICA, objeto.INFORMACIONREQ, objeto.NECESITA_MUESTRA, objeto.ID);
                        break;
                    //case 40:
                    //    sql2 = String.Format("UPDATE TER_W_WORKFLOWB2B SET MAT_PRECIO='{0}',MAT_CODIGO='{1}',MAT_PROVEDOR='{2}',MAT_LEADTIME='{3}',MAT_DENSIDAD='{4}',MAQ_INVERSION='{5}',MAQ_LEADTIME='{6}',ACC_INVERSION='{7}',ACC_LEADTIME='{8}',INS_PRECIO='{9}',INS_CODIGO='{10}',INS_LEADTIME='{11}',PAR_PROCESO='{12}',PAR_SETUP='{13}',PAR_VELOCIDAD='{14}',PA_DESPERDICIO_V='{15}',PAR_DESPERDICIO_F='{16}'  WHERE ID ={17} ",
                    //        objeto.MAT_PRECIO, objeto.MAT_CODIGO, objeto.MAT_PROVEDOR, objeto.MAT_LEADTIME, objeto.MAT_DENSIDAD, objeto.MAQ_INVERSION, objeto.MAQ_LEADTIME, objeto.ACC_INVERSION, objeto.ACC_LEADTIME, objeto.INS_PRECIO, objeto.INS_CODIGO, objeto.INS_LEADTIME, objeto.PAR_PROCESO, objeto.PAR_SETUP, objeto.PAR_VELOCIDAD, objeto.PA_DESPERDICIO_V, objeto.PAR_DESPERDICIO_F, objeto.ID);
                    //    break;
                    case 50:
                        sql2 = String.Format("UPDATE TER_W_WORKFLOWB2B SET UNIDAD_VENTA='{0}',	MARGEN='{1}',	LOTE_INDUSTRIAL ='{2}' WHERE ID ={3} ", objeto.UNIDAD_VENTA, String.IsNullOrEmpty(objeto.MARGEN) ? "" : objeto.MARGEN, objeto.LOTE_INDUSTRIAL, objeto.ID);
                        break;

                    case 80:
                        sql2 = String.Format("UPDATE TER_W_WORKFLOWB2B SET REFERENCIA_COSTEO='{0}', CELULA_TECNICA='{1}' WHERE ID={2} ", objeto.COTIZACION, objeto.CELULA_TECNICA, objeto.ID);
                        break;

                }
                if (ValidarSiguiente(objeto.ID,objeto.PASO_ACTUAL))
                {
                    string sql = @" EXEC TER_W_SIGUIENTE @ID,@TIPO,@PASO_ACTUAL,@USUARIO";
                    using (TERMOAPPEntities db = new TERMOAPPEntities())
                    {
                        using (var dbContextTransaction = db.Database.BeginTransaction())
                        {


                            db.Database.ExecuteSqlCommand(sql,
                            new SqlParameter("@ID", objeto.ID),
                            new SqlParameter("@TIPO", objeto.TIPO),
                            new SqlParameter("@PASO_ACTUAL", objeto.PASO_ACTUAL),
                            new SqlParameter("@USUARIO", String.IsNullOrEmpty(usuario) ? "" : usuario)
                            //new SqlParameter("@ALTERNO", 0)
                            );
                            db.Database.ExecuteSqlCommand(sql2);
                            db.SaveChanges();
                            dbContextTransaction.Commit();



                        }
                    }
                    DateTime now = DateTime.Now;
                    envioCorreo(objeto);

                    return Json(data: new { success = true, message = "Datos Ingresados" }, JsonRequestBehavior.AllowGet);
                }
                else { return Json(data: new { success = false, message = "Informacion Requerida Pendiente" }, JsonRequestBehavior.AllowGet); }

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Insertar", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public bool CheckWorkflowData(int id)
        {
            bool hasMissingData;

            using (TERMOAPPEntities db = new TERMOAPPEntities())
            {
               
                    // ... Tu código existente ...

                    // Llamada al procedimiento almacenado para verificar los datos del flujo de trabajo
                    using (var cmd = db.Database.Connection.CreateCommand())
                    {
                        cmd.CommandText = "CheckWorkflowData";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        var idParam = new SqlParameter("@ID", System.Data.SqlDbType.Int);
                        idParam.Value = id;
                        cmd.Parameters.Add(idParam);

                        var hasMissingDataParam = new SqlParameter("@VALOR", System.Data.SqlDbType.Bit);
                        hasMissingDataParam.Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters.Add(hasMissingDataParam);

                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        hasMissingData = (bool)hasMissingDataParam.Value;
                    }

                    
                
            }

            return !hasMissingData; // Si hasMissingData es verdadero, faltan datos, por lo que invertimos el valor.
        }
        public bool ValidarSiguiente(int id,int siguiente) 
        {
            Boolean respuesta = false;
            if (siguiente==40)
            {
                respuesta= CheckWorkflowData(id);


            }
            else { respuesta = true; }

            return respuesta;
        }

        public JsonResult SiguienteArt(TER_W_WORFLOW_CARTICULO objeto)
        {
            try
            {
                string sql2 = "SELECT ''";

                if (objeto.PASO_ACTUAL.Equals(10))
                {
                    sql2 = String.Format("UPDATE TER_W_WORFLOW_CARTICULO SET COMENTARIOS='{0}' ,CANTIDAD_MAX='{1}' WHERE ID={2}",
                      String.IsNullOrEmpty(objeto.COMENTARIOS) ? "" : objeto.COMENTARIOS, String.IsNullOrEmpty(objeto.CANTIDAD_MAX.ToString()) ? "" : objeto.CANTIDAD_MAX.ToString(),objeto.ID);
                }

                if (objeto.PASO_ACTUAL.Equals(30)) 
                {
                    sql2 = String.Format("UPDATE TER_W_WORFLOW_CARTICULO SET VALIDACION='{0}' , CARTILLA_CALIDAD='{1}',NUM_CARTILLA='{2}',TIPO_PEDIDO='{3}',NOTAS_PEDIDO='{4}' WHERE ID={5}",
                      String.IsNullOrEmpty(objeto.VALIDACION) ? "" : objeto.VALIDACION, String.IsNullOrEmpty(objeto.CARTILLA_CALIDAD) ? "" : objeto.CARTILLA_CALIDAD, String.IsNullOrEmpty(objeto.NUM_CARTILLA) ? "" : objeto.NUM_CARTILLA,
                      String.IsNullOrEmpty(objeto.TIPO_PEDIDO) ? "" : objeto.TIPO_PEDIDO, String.IsNullOrEmpty(objeto.NOTAS_PEDIDO) ? "" : objeto.NOTAS_PEDIDO, objeto.ID);
                }
                else if (objeto.PASO_ACTUAL.Equals(40))
                           {
                    sql2 = String.Format("UPDATE TER_W_WORFLOW_CARTICULO SET FECHA_ENTREGA='{0}', FECHA_VALIDACION_IMP='{1}' WHERE ID={2} ", objeto.FECHA_ENTREGA, objeto.FECHA_VALIDACION_IMP, objeto.ID);

                }
                //ACA
                else if (objeto.PASO_ACTUAL.Equals(50))
                {
                    sql2 = String.Format("UPDATE TER_W_WORFLOW_CARTICULO SET OBSERVACIONES='{0}',ESTADO='{1}'  WHERE ID ={2} ",
                         objeto.OBSERVACIONES, objeto.ESTADO, objeto.ID);
                }
                else if (objeto.PASO_ACTUAL.Equals(60))
                {
                    sql2 = String.Format("UPDATE TER_W_WORFLOW_CARTICULO SET OBSERVACIONES='{0}',ESTADO='{1}'  WHERE ID ={2} ",
                         objeto.OBSERVACIONES, objeto.ESTADO, objeto.ID);
                }
                else if (objeto.PASO_ACTUAL.Equals(70))
                {
                    sql2 = String.Format("UPDATE TER_W_WORFLOW_CARTICULO SET OBSERVACIONES='{0}',ESTADO='{1}'  WHERE ID ={2} ",
                         objeto.OBSERVACIONES, objeto.ESTADO, objeto.ID);
                }
                envioCorreo2(objeto);
                string sql = @" EXEC TER_W_SIGUIENTE @ID,@TIPO,@PASO_ACTUAL,@USUARIO";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@TIPO", objeto.TIPO),
                        new SqlParameter("@PASO_ACTUAL", objeto.PASO_ACTUAL),
                        new SqlParameter("@USUARIO", String.IsNullOrEmpty(usuario) ? "" : usuario)
                        //new SqlParameter("@ALTERNO", 0)
                        );
                        db.Database.ExecuteSqlCommand(sql2);
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


        public JsonResult SiguienteMue(Muestras objeto)
        {
            try
            {
                string sql2 = "SELECT ''";

                if (objeto.PASO_ACTUAL.Equals(10))
                {
                    sql2 = String.Format("UPDATE TER_W_WORKFLOW_MUESTRA SET P_EQUIPO_TEC='{0}' ,EXP_MATERIALES='{1}' ,MUESTRA='{2}' ,CODIGO_U_MUESTRA='{3}' ,MAESTR_ING='{4}' ,DUI_MUESTRA='{5}' ,CANTIDAD='{6}',FORMULA='{7}' ,ESTRUCTURA='{8}' , COMENTARIOS2='{9}'   WHERE ID={10}",
                      String.IsNullOrEmpty(objeto.P_EQUIPO_TEC) ? "" : objeto.P_EQUIPO_TEC, String.IsNullOrEmpty(objeto.EXP_MATERIALES.ToString()) ? "" : objeto.EXP_MATERIALES.ToString(), String.IsNullOrEmpty(objeto.MUESTRA.ToString()) ? "" : objeto.MUESTRA.ToString()
                      , String.IsNullOrEmpty(objeto.CODIGO_U_MUESTRA.ToString()) ? "" : objeto.CODIGO_U_MUESTRA.ToString(), String.IsNullOrEmpty(objeto.MAESTR_ING.ToString()) ? "" : objeto.MAESTR_ING.ToString(), String.IsNullOrEmpty(objeto.DUI_MUESTRA.ToString()) ? "" : objeto.DUI_MUESTRA.ToString()
                      , String.IsNullOrEmpty(objeto.CANTIDAD.ToString()) ? "" : objeto.CANTIDAD, String.IsNullOrEmpty(objeto.FORMULA.ToString()) ? "" : objeto.FORMULA.ToString(), String.IsNullOrEmpty(objeto.ESTRUCTURA.ToString()) ? "" : objeto.ESTRUCTURA.ToString(), String.IsNullOrEmpty(objeto.COMENTARIOS2) ? "" : objeto.COMENTARIOS2,objeto.ID);
                }

                if (objeto.PASO_ACTUAL.Equals(20))
                {
                    sql2 = String.Format("UPDATE TER_W_WORKFLOW_MUESTRA SET OT='{0}' , FECHA_EST_ENTRAGA='{1}' WHERE ID={2}",
                      String.IsNullOrEmpty(objeto.OT) ? "" : objeto.OT, String.IsNullOrEmpty(objeto.FECHA_EST_ENTRAGA) ? "" : objeto.FECHA_EST_ENTRAGA, objeto.ID);
                }
                if (objeto.PASO_ACTUAL.Equals(30))
                {
                    sql2 = String.Format("UPDATE TER_W_WORKFLOW_MUESTRA SET TP_CANTIDAD='{0}' , COMENTARIOS='{1}' WHERE ID={2}",
                      String.IsNullOrEmpty(objeto.TP_CANTIDAD.ToString()) ? "" : objeto.TP_CANTIDAD, String.IsNullOrEmpty(objeto.COMENTARIOS) ? "" : objeto.COMENTARIOS, objeto.ID);
                }

                if (objeto.PASO_ACTUAL.Equals(40))
                {
                    sql2 = String.Format("UPDATE TER_W_WORKFLOW_MUESTRA SET APROBADA='{0}' , FECHA_VALIDACION_C='{1}' WHERE ID={2}",
                      String.IsNullOrEmpty(objeto.APROBADA.ToString()) ? "" : objeto.APROBADA, String.IsNullOrEmpty(objeto.FECHA_VALIDACION_C.ToString()) ? "" : objeto.FECHA_VALIDACION_C, objeto.ID);
                    if (objeto.APROBADA.Equals("NO")) 
                    {
                        objeto.PASO_ACTUAL = 0;
                    }

                }
                if (objeto.PASO_ACTUAL.Equals(50))
                {
                    sql2 = String.Format("UPDATE TER_W_WORKFLOW_MUESTRA SET PASO='{0}' , CANTIDAD_LOTE='{1}' WHERE ID={2}",
                      String.IsNullOrEmpty(objeto.PASO) ? "" : objeto.PASO, String.IsNullOrEmpty(objeto.CANTIDAD_LOTE) ? "" : objeto.CANTIDAD_LOTE, objeto.ID);

                    
                }
                string sql = @" EXEC TER_W_SIGUIENTE @ID,@TIPO,@PASO_ACTUAL,@USUARIO";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {

                        db.Database.ExecuteSqlCommand(sql2);
                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@TIPO", "MUE"),
                        new SqlParameter("@PASO_ACTUAL", objeto.PASO_ACTUAL),
                        new SqlParameter("@USUARIO", String.IsNullOrEmpty(usuario) ? "" : usuario)
                        //new SqlParameter("@ALTERNO", 0)
                        );
                        
                        db.SaveChanges();
                        dbContextTransaction.Commit();



                    }
                }
                DateTime now = DateTime.Now;
                envioCorreo3(objeto);
                return Json(data: new { success = true, message = "Datos Ingresados" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Insertar", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SiguientePro(Produccion objeto)
        {
            try
            {
                string sql2 = "SELECT ''";

                if (objeto.PASO_ACTUAL.Equals(10))
                {
                    sql2 = String.Format("UPDATE TER_W_SERVICIOS_PRODUCCION SET TRABAJO_REALIZADO='{0}' ,COMENTARIOS='{1}'   WHERE ID={2}",
                      String.IsNullOrEmpty(objeto.TRABAJO_REALIZADO.ToString()) ? "" : objeto.TRABAJO_REALIZADO.ToString(), String.IsNullOrEmpty(objeto.COMENTARIOS.ToString()) ? "" : objeto.COMENTARIOS.ToString(), objeto.ID);
                }

                string sql = @" EXEC TER_W_SIGUIENTE @ID,@TIPO,@PASO_ACTUAL,@USUARIO";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {

                        db.Database.ExecuteSqlCommand(sql2);
                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@TIPO", objeto.TIPO),
                        new SqlParameter("@PASO_ACTUAL", objeto.PASO_ACTUAL),
                        new SqlParameter("@USUARIO", String.IsNullOrEmpty(usuario) ? "" : usuario)
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


        public JsonResult SiguienteOtro(Otros objeto)
        {
            try
            {
                string sql2 = "SELECT ''";

                
                string sql = @" EXEC TER_W_SIGUIENTE @ID,@TIPO,@PASO_ACTUAL,@USUARIO";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {

                        db.Database.ExecuteSqlCommand(sql2);
                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@TIPO", objeto.TIPO),
                        new SqlParameter("@PASO_ACTUAL", objeto.PASO_ACTUAL),
                        new SqlParameter("@USUARIO", String.IsNullOrEmpty(usuario) ? "" : usuario)
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




        public JsonResult SiguienteSar(SAR objeto)
        {
            try
            {

                string sql = @" EXEC TER_W_SIGUIENTE @ID,@TIPO,@PASO_ACTUAL,@USUARIO";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@TIPO", objeto.TIPO),
                        new SqlParameter("@PASO_ACTUAL", objeto.PASO_ACTUAL),
                        new SqlParameter("@USUARIO", String.IsNullOrEmpty(usuario) ? "" : usuario)
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


        public JsonResult Copiarworkflow(WORKFLOWB2B objeto)
        {
            try
            {

                string sql = @" EXEC TER_COPIAR @ID,@WORKFLOW";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID", objeto.ID),
                        new SqlParameter("@WORKFLOW", objeto.WORKFLOWS)
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



        public void envioCorreo(WORKFLOWB2B objeto)
        {
            DateTime now = DateTime.Now;
            string para = "", asunto = "", mensaje = "";
            //para = "celulacxp@termo.com.sv";
            switch (objeto.PASO_ACTUAL)
            {
                case 10:
                    para = correos.ObtenerCorreo(objeto.TIPO, objeto.PASO_ACTUAL);//mangandi
                    asunto = String.Format("Nuevo proyecto {0}", objeto.TITULO);
                    mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br> " + objeto.TITULO + " <br> Fecha: " + now.ToString("dd/MM/yyyy");
                    break;
                case 20:
                    //para = "mmangandi@ternova.group;soporte.tecnico2@ternova.group;ceramirez@ternova.group";//mangandi
                    
                    para = correos.ObtenerCorreo(objeto.TIPO, objeto.PASO_ACTUAL);//mangandi
                    asunto = String.Format("EDUI {0} CORRESPONDIENTE AL", objeto.DUI, objeto.TITULO);
                    mensaje = " El usuario " + usuario + "<br> <strong> ha creado numero de EDUI: </strong>" + objeto.DUI + " " +
                        "<br> <strong>Correspondiente al proyecto: </strong>" + objeto.TITULO+
                        "<br><strong> Fecha:</strong> " + now.ToString("dd/MM/yyyy");
                    break;
                case 30:
                    para = correos.ObtenerCorreo(objeto.TIPO, objeto.PASO_ACTUAL);//
                    if (objeto.CELULA_TECNICA.Equals("SI")) 
                    {
                        asunto = String.Format("Consulta cvt del proyecto: {0}", objeto.TITULO);
                        mensaje = "<strong>Es necesario completar la información: </strong> " + objeto.INFORMACIONREQ+ " <br> <strong> En el EDUI:</strong>  " + objeto.DUI + "<strong>  <br> del Proyecto:</strong>  " + objeto.TITULO + "<strong>  <br> Fecha:</strong>  " + now.ToString("dd/MM/yyyy");
                     break;
                    }

                  
            
                    else { asunto = String.Format("INFORMACION COMPLETA PARA COTIZAR"); }

                    mensaje = "Se ha completado la información Edui: <br> " + objeto.DUI + "del Proyecto <br> " + objeto.TITULO + " <br> Fecha: " + now.ToString("dd/MM/yyyy");
                    break;

                case 40:
                    para = correos.ObtenerCorreo(objeto.TIPO, objeto.PASO_ACTUAL);//
                    asunto = String.Format("INFORMACION COMPLETA CORRESPONDIENTE PARA COTIZAR DUI:{0}", objeto.DUI);
                    mensaje = "Se ha completado la información <br> <strong> para el DUI:</strong> " + objeto.DUI + "<br><strong>para el Proyecto</strong> " + objeto.TITULO + " <br><strong>  Fecha:</strong>  " + now.ToString("dd/MM/yyyy");
                    break;
                case 50:
                    para = correos.ObtenerCorreo(objeto.TIPO, objeto.PASO_ACTUAL);//
                    asunto = String.Format("Cotizacion Generada {0}-{1}", objeto.IDCLIENTE, objeto.TITULO);
                    mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br><strong> Referencia de Cotizacion</strong>" + objeto.COTIZACION + "<br>" + objeto.TITULO + " <br><strong> Fecha: </strong>" + now.ToString("dd/MM/yyyy");
                    break;
                case 60:
                    para = correos.ObtenerCorreo(objeto.TIPO, 50);//
                    asunto = String.Format("Cotizacion Completa correspondiente a Proyecto {0} ", objeto.TITULO);
                    mensaje = " <strong>Referencia de cotizacion </strong>" + objeto.COTIZACION + "<br> Notas:  "+objeto.NOTASREQ
                        + "<br> <strong>Correspondiente al DUI: </strong> " + objeto.DUI +
                        "<br><strong> Que pertenece al proyecto:</strong>  " + objeto.TITULO +
                        "<br><strong> Necesita Muestra: </strong> " + (objeto.NECESITA_MUESTRA=="1"?"SI":"NO") +
                        " <br> <strong>Fecha:</strong> " + now.ToString("dd/MM/yyyy");
                   break;
                case 80:
                    para = correos.ObtenerCorreo(objeto.TIPO,80);//
                    asunto = String.Format("Cotizacion Completa correspondiente a Proyecto {0}  ",objeto.TITULO);
                    mensaje = " <strong>Referencia de cotizacion</strong> " + objeto.COTIZACION + "<br> <strong>Notas: </strong> " + objeto.NOTASREQ
                        + "<br> <strong>Correspondiente al DUI: </strong> " + objeto.DUI +
                        "<br> <strong>Que pertenece al proyecto: </strong> " + objeto.TITULO +
                        "<br> <strong>Necesita Muestra:</strong>  " + (objeto.NECESITA_MUESTRA == "1" ? "SI" : "NO") +
                        " <br><strong> Fecha: </strong>" + now.ToString("dd/MM/yyyy");
                    break;


            }
            correo( para,  asunto, mensaje);

        }


        public void envioCorreo3(Muestras objeto)
        {
            DateTime now = DateTime.Now;
            string para = "", asunto = "", mensaje = "";
            //para = "celulacxp@termo.com.sv";
            switch (objeto.PASO_ACTUAL)
            {

                case 0:
                    //para = "mmangandi@ternova.group;soporte.tecnico2@ternova.group;ceramirez@ternova.group";//mangandi
                    para = correos.ObtenerCorreo("MUE", (Int32)objeto.PASO_ACTUAL);//mangandi
                    asunto = String.Format("Workflow Muestras -{0}-{1}-{2} ", "", objeto.IDCLIENTE, objeto.PRODUCTO,objeto.CANTIDAD);
                    mensaje = " Buen día, se ha ingresado requerimiento de muestras <br> DUI: "+ objeto.DUI
                        + $"<br> <b>Cliente: </b> {objeto.IDCLIENTE} <br> "
                        + $" <b>Nombre Producto: </b> {objeto.NOMBRE_PRODUCTO} <br> "
                        + $" <b>Tipo Proyecto: </b> {objeto.TIPO_PROYECTO} <br> "
                        + $" <b>Cantidad Muestra Requerida: </b> {objeto.CANT_MUESTRA_REQ} <br> "
                        + $" <b>Tipo Impresion: </b> {objeto.TIPO_IMPRESION} <br> "
                        + $"<b>Empaque para Alimentos: </b> {(objeto.EMPAQUE_ALIMENTOS ? "SI" : "NO")} <br>"
                        + $" <b>Fecha Requerida: </b> {objeto.FECHA_REQUERIDA} <br> "
                        + "";
                    break;
                case 10:
                    para = correos.ObtenerCorreo("MUE", (Int32)objeto.PASO_ACTUAL);//mangandi
                    asunto = String.Format("Liberacion Muestra {0}-{1} ", "", objeto.IDCLIENTE, objeto.PRODUCTO);
                    mensaje = " Buen día, se ha ingresado un nuevo pedido de muestra para producción. <br> <b>DUI:</b> " + objeto.DUI
                        + $"<br> <b>Muestra: </b> {objeto.MUESTRA} <br> "
                        + $" <b>Codigo Unico de Muestra: </b> {objeto.CODIGO_U_MUESTRA} <br> "
                        + $" <b>Numero de Dui Muestra: </b> {objeto.DUI_MUESTRA} <br> "
                        + $" <b>Maestro de Ingenieria: </b> {objeto.MAESTR_ING} <br> "
                        + $" <b>Cantidad: </b> {objeto.CANTIDAD} <br> "
                        + $"<b>Formula: </b> {objeto.FORMULA} <br> "
                        + $" <b>Estructura: </b> {objeto.ESTRUCTURA} <br> "
                        + $" <b>Presencia de equipo Tecnico: </b> {objeto.P_EQUIPO_TEC} <br> "
                        + $" <b>Explosion de Materiales: </b> {objeto.EXP_MATERIALES} <br> "
                        + $" <b>Comentarios: </b> {objeto.COMENTARIOS2} <br> "
                        + "";
                    break;
                case 20:
                    para = correos.ObtenerCorreo("MUE", (Int32)objeto.PASO_ACTUAL);//mangandi
                    asunto = String.Format("Liberacion Muestra {0}-{1} ", "", objeto.IDCLIENTE, objeto.PRODUCTO);
                    mensaje = $"Buen día, orden de trabajo liberada<br>"
                        + $" <b>Orden de Trabajo: </b> {objeto.OT} <br> "
                        + $" <b>Fecha Estimada de Entrega: </b> {objeto.FECHA_EST_ENTRAGA} <br> "
                        + "";
                    break;
                case 30:
                    para = correos.ObtenerCorreo("MUE", (Int32)objeto.PASO_ACTUAL);//mangandi
                    asunto = String.Format("Liberacion Muestra {0}-{1} ", "", objeto.IDCLIENTE, objeto.PRODUCTO);
                    mensaje = " Buen día, Se comparte liberación de muestra se envía la documentación correspondiente.<br>"
                        + $" <b>Cantidades: </b> {objeto.TP_CANTIDAD} <br> "
                        + $" <b>comentarios: </b> {objeto.COMENTARIOS} <br> "
                        + "";

                    break;
                case 40:
                    para = correos.ObtenerCorreo("MUE", (Int32)objeto.PASO_ACTUAL);//mangandi
                    asunto = String.Format("Aprobacion {0}-{1} ", "", objeto.IDCLIENTE, objeto.PRODUCTO);
                    mensaje = " Buen día, se comparte información de la validación de muestra.<br>"
                    + $" <b>Aprobada: </b> {objeto.APROBADA} <br> "
                     + $" <b>Fecha de Validacion: </b> {objeto.FECHA_VALIDACION_C} <br> "
                        + "";
                    break;

                case 50:
                    para = correos.ObtenerCorreo("MUE", (Int32)objeto.PASO_ACTUAL);//mangandi
                    asunto = String.Format("Aprobacion {0}-{1} ", "", objeto.IDCLIENTE, objeto.PRODUCTO);
                    mensaje = " Buen día, se ha solicitado creación de lote industrial <br>"
                     + $" <b>Paso: </b> {objeto.PASO} <br> "
                     + $" <b>Cantidad de Lote: </b> {objeto.CANTIDAD_LOTE} <br> "
                     + "";
                    break;

            }



            correo(para, asunto, mensaje);

        }


        public void envioCorreo4(Produccion objeto)
        {
            DateTime now = DateTime.Now;
            string para = "", asunto = "", mensaje = "";
            //para = "celulacxp@termo.com.sv";
            para = correos.ObtenerCorreo(objeto.TIPO, (Int32)objeto.PASO_ACTUAL);//mangandi
            asunto = String.Format("Tarea Workflow Articulo {0}- {1}",  objeto.IDCLIENTE, objeto.DESCRIPCION);
            mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br> " + objeto.DESCRIPCION + " <br> Fecha: " + now.ToString("dd/MM/yyyy");

            correo(para, asunto, mensaje);

        }
        public void envioCorreo5(SAR objeto)
        {
            DateTime now = DateTime.Now;
            string para = "", asunto = "", mensaje = "";
            //para = "celulacxp@termo.com.sv";
            para = correos.ObtenerCorreo(objeto.TIPO, (Int32)objeto.PASO_ACTUAL);//mangandi
            asunto = String.Format("Tarea Workflow  {0}",  objeto.ARTE);
            mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br> " + objeto.ARTE + " <br> Fecha: " + now.ToString("dd/MM/yyyy");

            correo(para, asunto, mensaje);

        }

        public void envioCorreo6(Otros objeto)
        {
            DateTime now = DateTime.Now;
            string para = "", asunto = "", mensaje = "";
            //para = "celulacxp@termo.com.sv";
            para = correos.ObtenerCorreo(objeto.TIPO, (Int32)objeto.PASO_ACTUAL);//mangandi
            asunto = String.Format("Tarea Workflow  {0}", objeto.IDCLIENTE);
            mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br> " + objeto.IDCLIENTE + " <br> Fecha: " + now.ToString("dd/MM/yyyy");

            correo(para, asunto, mensaje);

        }

        public void envioCorreo2(TER_W_WORFLOW_CARTICULO objeto)
        {
            DateTime now = DateTime.Now;
            string para = "", asunto = "", mensaje = "";
            //para = "celulacxp@termo.com.sv";
            //switch (objeto.PASO_ACTUAL)
            //{

            //    case 10:
            //        //para = "mmangandi@ternova.group;soporte.tecnico2@ternova.group;ceramirez@ternova.group";//mangandi
            //        para = correos.ObtenerCorreo(objeto.TIPO, (Int32)objeto.PASO_ACTUAL);//mangandi
            //        asunto = String.Format("Tarea Workflow Articulo ", "", objeto.IDCLIENTE, objeto.TITULO);
            //        mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br> " + objeto.TITULO + " <br> Fecha: " + now.ToString("dd/MM/yyyy");
            //        break;
            //    case 20:
            //        para = correos.ObtenerCorreo(objeto.TIPO, (Int32)objeto.PASO_ACTUAL);//mangandi
            //        asunto = String.Format("Tarea Workflow Articulo ", "", objeto.IDCLIENTE, objeto.TITULO);
            //        mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br> " + objeto.TITULO + " <br> Fecha: " + now.ToString("dd/MM/yyyy");
            //        break;
            //    case 30:
            //        para = correos.ObtenerCorreo(objeto.TIPO, (Int32)objeto.PASO_ACTUAL);//mangandi
            //        asunto = String.Format("Tarea Workflow Articulo ", "", objeto.IDCLIENTE, objeto.TITULO);
            //        mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br> " + objeto.TITULO + " <br> Fecha: " + now.ToString("dd/MM/yyyy");
            //        break;
            //    case 40:
            //        para = correos.ObtenerCorreo(objeto.TIPO, (Int32)objeto.PASO_ACTUAL);//mangandi
            //        asunto = String.Format("Tarea Workflow Articulo ", "", objeto.IDCLIENTE, objeto.TITULO);
            //        mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br> " + objeto.TITULO + " <br> Fecha: " + now.ToString("dd/MM/yyyy");
            //        break;
            //    case 50:
            //        para = correos.ObtenerCorreo(objeto.TIPO, (Int32)objeto.PASO_ACTUAL);//mangandi
            //        asunto = String.Format("Tarea Workflow Articulo ", "", objeto.IDCLIENTE, objeto.TITULO);
            //        mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br> " + objeto.TITULO + " <br> Fecha: " + now.ToString("dd/MM/yyyy");
            //        break;


            //}

            para = correos.ObtenerCorreo(objeto.TIPO, (Int32)objeto.PASO_ACTUAL);//mangandi
            asunto = String.Format("Tarea Workflow Articulo ", "", objeto.IDCLIENTE, objeto.TITULO);
            mensaje = " El usuario " + usuario + " ha iniciado  un nuevo proyecto <br> " + objeto.TITULO + " <br> Fecha: " + now.ToString("dd/MM/yyyy");
           
            correo(para, asunto, mensaje);

        }

        
        public bool correo(string para , string asunto, string mensaje )
        {
            try
            {
                


                Log.Info(String.Format("Inicio de envio de correo: {0}", mensaje));
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("aplicativos@Ternova.group", "Aplicativos");
                correo.To.Add(para);
                //correo.To.Add("soporte.tecnico2@ternova.group;ceramirez@ternova.group;mmangandi@ternova.group;mjsanchez@termo.com.sv;rbonilla@ternova.group");
                //correo.To.Add("soporte.tecnico2@ternova.group");
                correo.Subject = asunto;
                correo.Body = mensaje;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                //configuracion smtmp
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp3.termonet.com";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.Send(correo);
                return true;

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error Enviado correo:  {0}", ex.Message.ToString()));
                return false;

            }
        }


   

        [HttpGet]
        public JsonResult Ver(int idworkflow,string tipo)
        {
            DataSet ds = new DataSet();

            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "SELECT Id,Convert(varchar,Creado,120) Creado,concat(Nombre,'.',Extension) as Nombre,IdWorkflow,Extension,Descargas FROM TER_W_ADJUNTOS where IdWorkflow=" + idworkflow + " and tipo='"+tipo+"'";
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
            .Select(dataRow => new Archivo
            {

                Id = dataRow.Field<Guid>("ID"),
                Nombre = dataRow.Field<string>("Nombre"),
                IdWorkflow = dataRow.Field<Int32>("IdWorkflow"),
                Extension = dataRow.Field<string>("Creado"),
                Descargas = dataRow.Field<Int32>("Descargas")
                
            }).ToList();
            //using (_dbContext = new AppDbContext())
            //{
            //    //// Recuperamos la Lista de los archivos subidos
            //    //_archivos = _dbContext.Archivos.Where(x=> x.IdWorkflow.Equals(idworkflow)).OrderBy(x => x.Creado).Select(p => new Archivo
            //    //{
            //    //    Id = p.Id,
            //    //    Creado = p.Creado,
            //    //    Nombre = p.Nombre,
            //    //    IdWorkflow = p.IdWorkflow,
            //    //    Extension = p.Extension,
            //    //    Descargas = p.Descargas

            //    //}).ToList();
            //}
            // Retornamos la Vista Index, con los archivos subidos.
            return Json(new { data = Data }, JsonRequestBehavior.AllowGet);
           
        }

        [HttpPost]
        public JsonResult SubirArchivo()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                Int32 IdWorkflow=0;string atipo="";
                HttpPostedFileBase file = Request.Files[i];
                NameValueCollection nvc = Request.Form;
                if (!string.IsNullOrEmpty(nvc["idWorkflow"]))
                {
                    IdWorkflow = Int32.Parse(nvc["idWorkflow"]);
                }
                if (!string.IsNullOrEmpty(nvc["tipo"]))
                {
                    atipo = (nvc["tipo"]).ToString();
                }


                if (file != null && file.ContentLength > 0)
                {
                    // Extraemos el contenido en Bytes del archivo subido.
                    var _contenido = new byte[file.ContentLength];
                    file.InputStream.Read(_contenido, 0, file.ContentLength);

                    // Separamos el Nombre del archivo de la Extensión.
                    int indiceDelUltimoPunto = file.FileName.LastIndexOf('.');
                    string _nombre = file.FileName.Substring(0, indiceDelUltimoPunto);
                    string _extension = file.FileName.Substring(indiceDelUltimoPunto + 1,
                                        file.FileName.Length - indiceDelUltimoPunto - 1);

                    // Instanciamos la clase Archivo y asignammos los valores.
                    Archivo _archivo = new Archivo()
                    {
                        Nombre = _nombre,
                        Extension = _extension,
                        Descargas = 0,
                        IdWorkflow = IdWorkflow,
                        tipo=atipo
                    };

                    try
                    {
                        // Subimos el archivo al Servidor.
                        _archivo.SubirArchivo(_contenido);
                        // Guardamos en la base de datos la instancia del archivo
                        using (_dbContext = new AppDbContext())
                        {
                            _dbContext.Archivos.Add(_archivo);
                            _dbContext.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Aquí el código para manejar la Excepción.
                    }
                    return Json(data: new { success = false, message = "Archivo Adjuntado" }, JsonRequestBehavior.AllowGet);
                }
            }
            // Redirigimos a la Acción 'Index' para mostrar
            // Los archivos subidos al Servidor.
            return Json(data: new { success = false, message = "Debe seleccionar un Archivo" }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public JsonResult Comentar()
        {
            
                string comentarios = ""; string cantidades = "";
                
                NameValueCollection nvc = Request.Form;
                if (!string.IsNullOrEmpty(nvc["comentarios"]))
                {
                    comentarios = (nvc["comentarios"]);
                }
                if (!string.IsNullOrEmpty(nvc["cantidades"]))
                {
                    cantidades = (nvc["cantidades"]).ToString();
                }


               try
                    {
                    return Json(data: new { success = false, message = "Archivo Adjuntado" }, JsonRequestBehavior.AllowGet);
                }
                    catch (Exception ex)
                    {
                    // Aquí el código para manejar la Excepción.
                    return Json(data: new { success = false, message = "Archivo Adjuntado" }, JsonRequestBehavior.AllowGet);
                }
                    return Json(data: new { success = false, message = "Archivo Adjuntado" }, JsonRequestBehavior.AllowGet);
                
            
        }
           
        



        [HttpGet]
        public ActionResult DescargarArchivo(Guid id)
        {
            Archivo _archivo;
            FileContentResult _fileContent;

            using (_dbContext = new AppDbContext())
            {
                _archivo = _dbContext.Archivos.FirstOrDefault(x => x.Id == id);
            }

            if (_archivo == null)
            {
                return HttpNotFound();
            }
            else
            {
                try
                {
                    // Descargamos el archivo del Servidor.
                    _fileContent = new FileContentResult(_archivo.DescargarArchivo(),
                                                         "application/octet-stream");
                    _fileContent.FileDownloadName = _archivo.Nombre + "." + _archivo.Extension;

                    // Actualizamos el nº de descargas en la base de datos.
                    using (_dbContext = new AppDbContext())
                    {
                        _archivo.Descargas++;
                        _dbContext.Archivos.Attach(_archivo);
                        _dbContext.Entry(_archivo).State = EntityState.Modified;
                        _dbContext.SaveChanges();
                    }

                    return _fileContent;
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpGet]
        public JsonResult EliminarArchivo(String valor)
        {
            Archivo _archivo;
            Guid id = Guid.Parse(valor);

            using (_dbContext = new AppDbContext())
            {
                _archivo = _dbContext.Archivos.FirstOrDefault(x => x.Id == id);
            }

            if (_archivo != null)
            {
                using (_dbContext = new AppDbContext())
                {
                    _archivo = _dbContext.Archivos.FirstOrDefault(x => x.Id == id);
                    _dbContext.Archivos.Remove(_archivo);
                    if (_dbContext.SaveChanges() > 0)
                    {
                        // Eliminamos el archivo del Servidor.
                        _archivo.EliminarArchivo();
                    }
                }
                // Redirigimos a la Acción 'Index' para mostrar
                // Los archivos subidos al Servidor.
                return Json(data: new { success = true, message = "Archivo Eliminado" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(data: new { success = false, message = "no se pudo eliminar el Archivo" }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult MateriaPrima(int Id)
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new DataTables.Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_W_PARAMETROS_MAT", "ID")
                 .Where(q =>
                    q
                    .Where("IDWORKFLOW", Id, "=")
                     )
                .Model<TER_W_PARAMETROS_MAT>()
                .Field(new Field("ID"))
                .Field(new Field("IDWORKFLOW"))
                .Field(new Field("MAT_PRECIO"))
                .Field(new Field("MAT_CODIGO"))
                .Field(new Field("MAT_MATERIA"))
                .Field(new Field("MAT_PROVEDOR"))
                .Field(new Field("MAT_LEADTIME"))
                .Field(new Field("MAT_DENSIDAD"))
                .Field(new Field("ESTADO"))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }


        public ActionResult Maquina(int Id)
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new DataTables.Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_W_PARAMETROS_MAQ", "ID")
                 .Where(q =>
                    q
                    .Where("IDWORKFLOW", Id, "=")
                     )
                .Model<TER_W_PARAMETROS_MAQ>()
                .Field(new Field("ID"))
                .Field(new Field("IDWORKFLOW"))
                .Field(new Field("MAQ_INVERSION"))
                .Field(new Field("MAQ_LEADTIME"))
                .Field(new Field("DESCRIPCION"))
                .Field(new Field("ESTADO"))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult Accesorios(int Id)
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new DataTables.Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_W_PARAMETROS_ACC", "ID")
                 .Where(q =>
                    q
                    .Where("IDWORKFLOW", Id, "=")
                     )
                .Model<TER_W_PARAMETROS_ACC>()
                .Field(new Field("ID"))
                .Field(new Field("IDWORKFLOW"))
                .Field(new Field("ACC_INVERSION"))
                .Field(new Field("ACC_LEADTIME"))
                .Field(new Field("DESCRIPCION"))
                .Field(new Field("ESTADO"))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult Insumos(int Id)
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new DataTables.Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_W_PARAMETROS_INS", "ID")
                 .Where(q =>
                    q
                    .Where("IDWORKFLOW", Id, "=")
                     )
                .Model<TER_W_PARAMETROS_INS>()
                .Field(new Field("ID"))
                .Field(new Field("IDWORKFLOW"))
                .Field(new Field("INS_PRECIO"))
                .Field(new Field("INS_CODIGO"))
                .Field(new Field("INS_LEADTIME"))
                .Field(new Field("DESCRIPCION"))
                .Field(new Field("ESTADO"))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult Parametros(int Id)
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new DataTables.Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_W_PARAMETROS_PAR", "ID")
                 .Where(q =>
                    q
                    .Where("IDWORKFLOW", Id, "=")
                     )
                .Model<TER_W_PARAMETROS_PAR>()
                .Field(new Field("ID"))
                .Field(new Field("IDWORKFLOW"))
                .Field(new Field("PROCESO"))
                .Field(new Field("SETUP"))
                .Field(new Field("VELOCIDAD"))
                .Field(new Field("PA_DESPERDICIO_V"))
                .Field(new Field("PAR_DESPERDICIO_F"))
                .Field(new Field("ESTADO"))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }



       

    }
}