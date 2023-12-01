using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Apiweb.Properties;
using Apiweb.Utilidad;
using log4net;

namespace Apiweb.Models
{
    public class Correo
    {
        private static readonly ILog Log = Logs.GetLogger();
      

        // GET: WorkFlow
        Settings settings = Properties.Settings.Default;
        string usuario = SessionManager.GetUsuario();
        string entidad = SessionManager.GetEntidad();


        public String ObtenerCorreo(String tipo, int paso)
        {
            try
            {
                string sql = String.Format(@" EXEC SP_CORREO {0},'{1}'", paso, tipo);
                string constr = settings.DBConnection;
                string result = "";
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    connection.Open();

                    // Crear una nueva instancia de SqlCommand
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandType = CommandType.Text;

                    // Ejecutar el procedimiento almacenado
                    SqlDataReader reader = command.ExecuteReader();

                    // Leer el primer resultado
                    if (reader.Read())
                    {
                        result = reader[0].ToString();
                    }

                    connection.Close();
                }
                return result;


            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Obtener correos", ex.Message.ToString()));
                return "";
            }
        }


        public String ObtenerCorreoComercial(String tipo)
        {
            try
            {
                string sql = String.Format(@" EXEC [dbo].[SP_CORREO_COMERCIAL] '{0}'", tipo);
                string constr = settings.DBConnection;
                string result = "";
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    connection.Open();

                    // Crear una nueva instancia de SqlCommand
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.CommandType = CommandType.Text;

                    // Ejecutar el procedimiento almacenado
                    SqlDataReader reader = command.ExecuteReader();

                    // Leer el primer resultado
                    if (reader.Read())
                    {
                        result = reader[0].ToString();
                    }

                    connection.Close();
                }
                return result;


            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error al Obtener correos", ex.Message.ToString()));
                return "";
            }
        }
    }
}