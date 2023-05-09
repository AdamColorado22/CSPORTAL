using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class Otros
    {
        public Int32 ID { get; set; }
        public string TIPO { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string IDCLIENTE { get; set; }
        public string DUI { get; set; }
        public string CANTIDA_MAT_C { get; set; }
        public string CANTIDAD_DUMMIES { get; set; }
        public string ESPECIFICACIONES { get; set; }
        public string ASIGNADA { get; set; }
        public Int32 PASO_ACTUAL { get; set; }
        public string PROCESO_ACTUAL { get; set; }
        public string FECHA_INICIO { get; set; }
        public decimal TIEMPO_PROCESO { get; set; }
        public decimal TIEMPO_TOTAL { get; set; }
        public string FECHA_ESTIMADA { get; set; }
        public string FECHA_FINALIZACION { get; set; }
        public Int32 DIAS_TOTALES { get; set; }
 
        public string FECHA_REQUERIDA { get; set; }

    }
}