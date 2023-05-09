using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class SAR
    {
        public Int32 ID { get; set; }
        public string TIPO { get; set; }

        public string ARTE { get; set; }

        public string DUI { get; set; }

        public string RETOQUE { get; set; }

        public string IMPRESION { get; set; }
        

        public string ASIGNADO { get; set; }
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