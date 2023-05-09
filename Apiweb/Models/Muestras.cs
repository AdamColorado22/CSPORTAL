using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class Muestras
    {
        public Int32 ID { get; set; }
        public string PRODUCTO { get; set; }
        public string DUI { get; set; }
        public string IDCLIENTE { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public string TIPO_PROYECTO { get; set; }
        public string FECHA_REQUERIDA { get; set; }
        public string MUESTRA { get; set; }
        public string CODIGO_U_MUESTRA { get; set; }
        public string PART_ID { get; set; }
        public string DUI_MUESTRA { get; set; }
        public string MAESTR_ING { get; set; }
        public string NUM_PEDIDO { get; set; }
        public int PASO_ACTUAL { get; set; }
        public Nullable<decimal> CANTIDAD { get; set; }
        public string FORMULA { get; set; }
        public string P_EQUIPO_TEC { get; set; }
        public string EXP_MATERIALES { get; set; }
        public string OT { get; set; }
        public string FECHA_EST_ENTRAGA { get; set; }
        public Nullable<decimal>  TP_CANTIDAD { get; set; }
        public string PROCESO_ACTUAL { get; set; }
        public string FECHA_INICIO { get; set; }
        public Nullable<decimal> TIEMPO_PROCESO { get; set; }
        public Nullable<decimal> TIEMPO_TOTAL { get; set; }
        public string CANTIDAD_LOTE { get; set; }
        public string FECHA_ESTIMADA { get; set; }
        public string PASO { get; set; }
        public string FECHA_FINALIZACION { get; set; }
        
            public string COMENTARIOS { get; set; }
        public string APROBADA { get; set; }
        public string FECHA_VALIDACION_C { get; set; }
        public Int32 DIAS_TOTALES { get; set; }
        public string ESTRUCTURA { get; set; }
        

    }
}