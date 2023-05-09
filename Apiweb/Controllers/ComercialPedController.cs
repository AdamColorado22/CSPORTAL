using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Apiweb.Models;
using Apiweb.Properties;
using Dapper;
using DataTables;
using log4net;

namespace Apiweb.Controllers
{
    public class ComercialPedController : Controller
    {
        // GET: ComercialPed
        private static readonly ILog Log = Logs.GetLogger();
        AppDbContext _dbContext;
        Correo correos = new Correo();
        // GET: WorkFlow
        Settings settings = Properties.Settings.Default;
        string usuario = (System.Web.HttpContext.Current.Session["User"] as String).ToUpper();
        string entidad = System.Web.HttpContext.Current.Session["Base"] as String;
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Autorizador()
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


                var response = new Editor(db, "TER_COM_PEDIDOS", "ID")
                .Where(q =>
                q
                .Where("ESTADO", "GENERADO", "<>")
                .AndWhere("IDUSUARIO","%"+ usuario+"%", "LIKE")
                .AndWhere("BASE", entidad, "=")
                )
                .Model<TER_COM_PEDIDOS>()
                .Field(new Field("ID"))
                .Field(new Field("PEDIDO"))
                .Field(new Field("ESTADO"))
                .Field(new Field("FECHA_INGRESO"))
                .Field(new Field("CLIENTE"))
                .Field(new Field("OC"))
                .Field(new Field("TIPO"))
                .Field(new Field("PART_ID"))
                .Field(new Field("FECHA_ENTREGA"))
                .Field(new Field("CANTIDAD"))
                .Field(new Field("PRECIO"))
                .Field(new Field("REF_COTIZACION"))
                .Field(new Field("DUI"))
                .Field(new Field("SEGMENTO"))
                .Field(new Field("CLUSTER"))
                .Field(new Field("ESTADO_PED"))
                .Field(new Field("COSTO"))
                .Field(new Field("COMMODITY_CODE"))
                .Field(new Field("UN"))
                .Field(new Field("FC"))
                .Field(new Field("IDUSUARIO")
                .SetValue(usuario))
                .Field(new Field("BASE")
                .SetValue(entidad))
                .Field(new Field("MARGEN"))
                .Field(new Field("CARTILLA"))

                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }



        public ActionResult ListarTodo()
        {
            return View();
        }
        public ActionResult ListarTodoV()
        {

            Log.Info("Listando Lista Comercial");
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new DataTables.Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_COM_PEDIDOS", "ID")
                .Model<TER_COM_PEDIDOS>()
                .Field(new Field("ID"))
                .Field(new Field("PEDIDO"))
                .Field(new Field("ESTADO"))
                .Field(new Field("FECHA_INGRESO"))
                .Field(new Field("CLIENTE"))
                .Field(new Field("OC"))
                .Field(new Field("TIPO"))
                .Field(new Field("PART_ID"))
                .Field(new Field("FECHA_ENTREGA"))
                .Field(new Field("CANTIDAD"))
                .Field(new Field("PRECIO"))
                .Field(new Field("REF_COTIZACION"))
                .Field(new Field("DUI"))
                .Field(new Field("SEGMENTO"))
                .Field(new Field("CLUSTER"))
                .Field(new Field("COSTO"))
                .Field(new Field("COMMODITY_CODE"))
                .Field(new Field("UN"))
                .Field(new Field("FC"))
                .Field(new Field("IDUSUARIO") )
                .Field(new Field("BASE"))
                .Field(new Field("MARGEN"))
                .Field(new Field("CARTILLA"))

                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }



        public async Task<JsonResult> AutoComplete(string valor)
        {
            using (var con = new SqlConnection(settings.DBConnection))
            {
                var query = "SELECT ID, NAME as DESCRIPCION FROM " + entidad + ".DBO.CUSTOMER WHERE ID LIKE @Valor OR NAME LIKE @Valor";
                var custlist = await con.QueryAsync<Autocomplete>(query, new { Valor = "%" + valor + "%" });
                return Json(custlist.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Listar2()
        {

            Log.Info("Listando Lista Comercial");
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new DataTables.Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_COM_PEDIDOS", "ID")
                .Where(q =>
                q
                .Where("ESTADO", "PENDIENTE", "=")
                .AndWhere("BASE", entidad, "=")
                )
                .Model<TER_COM_PEDIDOS>()
                .Field(new Field("ID"))
                .Field(new Field("PEDIDO"))
                .Field(new Field("ESTADO"))
                .Field(new Field("FECHA_INGRESO"))
                .Field(new Field("CLIENTE"))
                .Field(new Field("OC"))
                .Field(new Field("TIPO"))
                .Field(new Field("PART_ID"))
                .Field(new Field("FECHA_ENTREGA"))
                .Field(new Field("CANTIDAD"))
                .Field(new Field("PRECIO"))
                .Field(new Field("REF_COTIZACION"))
                .Field(new Field("DUI"))
                .Field(new Field("SEGMENTO"))
                .Field(new Field("CLUSTER"))
                .Field(new Field("COSTO"))
                .Field(new Field("COMMODITY_CODE"))
                .Field(new Field("UN"))
                .Field(new Field("FC"))
                .Field(new Field("TOLERANCIA"))
                .Field(new Field("IDUSUARIO")
                .SetValue(usuario))
                .Field(new Field("ORDENCOMPRA"))
                .Field(new Field("BASE")
                .SetValue(entidad))
                .Field(new Field("MARGEN")
                .SetValue(0))

                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }



        public JsonResult Autorizar(List<TER_COM_PEDIDOS> pedidos)
        {
            try
            {
                string sql = @"EXEC TER_SP_COM_AUTORIZAR @USUARIO,@ID";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        foreach (var ped in pedidos)
                        {
                            correo(ped, 0, "");
                            db.Database.ExecuteSqlCommand(sql,
                            new SqlParameter("@USUARIO", usuario),
                            new SqlParameter("@ID", ped.ID));

                        }
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                }

                return Json(data: new { success = true, message = "Autorizacion enviada" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error Solicitar Autorizacion :{0}", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult Aprobar(List<TER_COM_PEDIDOS> pedidos)
        {
            try
            {
                string sql = @" UPDATE TER_COM_PEDIDOS SET ESTADO='AUTORIZADO' WHERE ID=@ID";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        foreach (var ped in pedidos)
                        {
                            correo(ped, 1, "");
                            db.Database.ExecuteSqlCommand(sql,
                            new SqlParameter("@ID", ped.ID));

                        }
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                }

                return Json(data: new { success = true, message = "Autorizacion enviada" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error Solicitar Autorizacion :{0}", ex.Message.ToString()));
                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }


        public bool correo(TER_COM_PEDIDOS ped, int numero, string pedido)
        {
            try
            {
                DateTime now = DateTime.Now;
                string para = "", asunto = "", mensaje = "";
                //para = "celulacxp@termo.com.sv";
                switch (numero)
                {
                    case 0:
                        //para = "soporte.tecnico2@ternova.group";//mangandi
                        para = correos.ObtenerCorreoComercial("Comercial");//mangandi
                        asunto = String.Format("Solicitud de Autorizacion de Pedido que no cumple MTC ");
                        mensaje = " El Sac " + usuario + " ha solicitado una autorizacion <br>Cliente: " + ped.CLIENTE + "<br> PART ID: " + ped.PART_ID + "<br>MTC: " + ped.MARGEN + " <br> Fecha: " + now.ToString("dd/MM/yyyy");
                        break;
                    case 1:
                        //para = "soporte.tecnico2@ternova.group";//mangandi
                        para = correos.ObtenerCorreoComercial("Aprobador");//mangandi
                        asunto = String.Format("Solicitud de MTC autorizada");
                        mensaje = " El Usuario " + usuario + " ha Aprobado su Solcitud <br>Cliente: " + ped.CLIENTE + "<br> PART ID: " + ped.PART_ID + "<br>MTC: " + ped.MARGEN + " <br> Fecha: " + now.ToString("dd/MM/yyyy");
                        break;
                    case 2:
                        //para = "soporte.tecnico2@ternova.group";//mangandi
                        para = correos.ObtenerCorreoComercial("Cartillas");//mangandi
                        asunto = String.Format("Cartilla de Color Solicitada");
                        mensaje = " El Usuario " + usuario + " Creado los Pedido :" + pedido + " <br> Solicita de Cartilla de Color <br> Fecha: " + now.ToString("dd/MM/yyyy");
                        break;
                }
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
                //smtp.Host = "192.168.60.47";
                smtp.Host = "172.16.1.17";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.Send(correo);
                return true;

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error Enviado correo: {0}", ex.Message.ToString()));
                return false;

            }
        }

        public void Cartillas(List<TER_COM_PEDIDOS> ped)
        {
            var tabla = new List<string>();
            TER_COM_PEDIDOS peds = new TER_COM_PEDIDOS();
            string ids = string.Join(",", ped.Select(x => x.ID));
            Log.Info(String.Format("Listando Pedidos con Cartilla", usuario));
            try
            {
                using (var connection = new SqlConnection(Settings.Default.DBConnection))
                {
                    connection.Open();
                    var query = String.Format("SELECT DISTINCT(PEDIDO) AS PEDIDO FROM TER_COM_PEDIDOS WHERE ID IN ({0}) and LEN(CARTILLA)>0", ids);
                    var command = new SqlCommand(query, connection);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tabla.Add(reader["PEDIDO"].ToString());
                    }
                    reader.Close();
                }
                if (tabla.Count > 0)
                {
                    string resultado = String.Join(",", tabla);
                    correo(peds, 2, resultado);
                }

            }


            catch (Exception ex)
            {
                Log.Error(String.Format("Error Consultando Archivos MT940 :{0}", ex.Message.ToString()));
            }
        }


        public JsonResult Pedido(List<TER_COM_PEDIDOS> pedidos)
        {
            try
            {
                string mensaje = "";
                string ids = string.Join(",", pedidos.Select(x => x.ID));

                string sql = @"EXEC TER_COM_CREA_PEDIDOS @ID_PED ,@USUARIO,@BASE";
                using (TERMOAPPEntities db = new TERMOAPPEntities())
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {


                        db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@ID_PED", ids),
                        new SqlParameter("@USUARIO", usuario),
                        new SqlParameter("@BASE", entidad));

                        db.SaveChanges();
                        dbContextTransaction.Commit();


                    }
                }
                Cartillas(pedidos);
                return Json(data: new { success = true, message = "Pedido Creado" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Crear Pedido :{0}", ex.Message.ToString()));

                return Json(data: new { success = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}