using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
    public class BodegaController : Controller
    {

        Settings settings = Properties.Settings.Default;
        // GET: Bodega

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Recibo()
        {
            return View();
        }



        public ActionResult Listar()
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;

            using (var db = new Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_BODEGA", "ID")
                 .Where(q =>
                    q
                    .Where("ESTADO", "I", "=")
                    //.Where("USUARIO_ENVIO", System.Web.HttpContext.Current.Session["User"] as String)
                     .OrWhere("ESTADO", "R", "=")
                    .Where("RECHAZO", "RECHAZADO", "=")

                    )
                .Model<TER_BODEGA>()
                .Field(new Field("ID"))
                .Field(new Field("PART_ID")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                .Field(new Field("WORKORDER_BASE_ID"))
                .Field(new Field("TIPO").SetValue("I"))
                .Field(new Field("CANTIDADENVIADA")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("CANTIDADRECIBIDA")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("CAJAS_ENVIO")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("CAJAS_RECIBO")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("CAJAS_PARCIALES")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("RECHAZO")
                .SetValue("PENDIENTE"))
                .Field(new Field("WAREHOUSE_ID")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("LOCATION_ID")
                .SetFormatter(Format.IfEmpty(null)))

               .Field(new Field("DESCRIPCION")
                .SetFormatter(Format.IfEmpty(null)))
               .Field(new Field("ESTADO")
                .SetValue("I"))
                .Field(new Field("FECHA")
                .SetFormatter(Format.IfEmpty(null)))

                .Field(new Field("USUARIO_ENVIO")
                
                .SetValue(System.Web.HttpContext.Current.Session["User"] as String))
                .Field(new Field("MILLARES_CAJA")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Process(formData)
                .Data();

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }


        public ActionResult Recibir()
        {
            var formData = HttpContext.Request.Form;
            var settings = Properties.Settings.Default;
    
            using (var db = new Database("sqlserver", settings.DBConnection))
            {


                var response = new Editor(db, "TER_BODEGA", "ID")
                 .Where(q =>
                    q
                    .Where("ESTADO", "I", "=")
                    .Where("RECHAZO", "PENDIENTE", "=")
                    .OrWhere("ESTADO", "R", "=")
                    .Where("RECHAZO", "PENDIENTE", "=")
                    )
                .Model<TER_BODEGA>()
                .Field(new Field("ID"))
                .Field(new Field("PART_ID")
                .Validator(Validation.NotEmpty(new ValidationOpts { Message = "Este Campo no puede estar Vacío" })))
                .Field(new Field("WORKORDER_BASE_ID"))
                .Field(new Field("TIPO").SetValue("I"))
                .Field(new Field("CANTIDADENVIADA")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null))
                )
                .Field(new Field("CANTIDADRECIBIDA")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))

                .Field(new Field("CAJAS_ENVIO")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("CAJAS_RECIBO")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("WAREHOUSE_ID")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("CAJAS_PARCIALES")
                .Validator(Validation.Numeric("es-SV", new ValidationOpts { Message = "Este Campo debe ser Numero" }))
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("RECHAZO")
                .SetFormatter(Format.IfEmpty(null)))

                .Field(new Field("LOCATION_ID")
                .SetFormatter(Format.IfEmpty(null)))

               .Field(new Field("DESCRIPCION")
                .SetFormatter(Format.IfEmpty(null)))
               .Field(new Field("ESTADO")
                .SetValue("R"))
                .Field(new Field("FECHA")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("USUARIO_ENVIO")
                .SetFormatter(Format.IfEmpty(null)))
                .Field(new Field("USUARIO_RECEPTOR")
                .SetValue(System.Web.HttpContext.Current.Session["User"] as String))
                
                .Process(formData)
                .Data();
               

                return Json(response, JsonRequestBehavior.AllowGet);

            }

        }

        public void valor(string valor) 
        {
            Console.WriteLine(valor);
        }



        public JsonResult AutoComplete(string tipo, string valor)
        {
            
            IList<String> intermediate_list = new List<String>();
            List<Autocomplete> custlist = null;
            DataSet ds = new DataSet();
            string constr = settings.DBConnection;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "EXEC TER_AUTOCOMPLETE_BOD  @TIPO,@VALOR";
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Parameters.Add("@TIPO", SqlDbType.VarChar, 30).Value = tipo;
                    cmd.Parameters.Add("@VALOR", SqlDbType.VarChar, 30).Value = "%" + valor + "%";
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }
            custlist = new List<Autocomplete>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Autocomplete cobj = new Autocomplete();
                cobj.ID = ds.Tables[0].Rows[i]["ID"].ToString();
                cobj.DESCRIPCION = ds.Tables[0].Rows[i]["DESCRIPCION"].ToString();
                custlist.Add(cobj);
            }

            return Json(custlist, JsonRequestBehavior.AllowGet);
        }



        public void CorreoRechazo( string base_id)
        {
            try
            {

                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("aplicativos@ternova.group", "Aplicativos");
                correo.To.Add("soporte.tecnico2@ternova.group");
                correo.Subject = "Rechazo de Recepcion";
                correo.Body = "Se ha Rechazado la recepcion de la OT <b>"+base_id+"</b>";
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                //configuracion smtmp
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp3.termonet.com";
                smtp.Port = 25;
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = true;
                smtp.Send(correo);
                

            }
            catch (Exception ex)
            {
               

            }
        }

    }
}