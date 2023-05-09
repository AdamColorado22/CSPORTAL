using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class Produccion
    {

        public Int32 ID { get; set; }
        public string TIPO { get; set; }
        public string TIPO_REQUERIMIENTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string IDCLIENTE { get; set; }
        public string DUI { get; set; }
   
        public string PERSONA_ASIGNADA { get; set; }
        public string TRABAJO_REALIZADO { get; set; }
        public string COMENTARIOS { get; set; }

        //---Todos
        public int PASO_ACTUAL { get; set; }
        
        public string PROCESO_ACTUAL { get; set; }
        public string FECHA_INICIO { get; set; }
        public Nullable<decimal> TIEMPO_PROCESO { get; set; }
        public Nullable<decimal> TIEMPO_TOTAL { get; set; }
        public string FECHA_ESTIMADA { get; set; }
        public string FECHA_FINALIZACION { get; set; }
        public string FECHA_REQUERIDA { get; set; }
        
        public Int32 DIAS_TOTALES { get; set; }

    }
}