using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class WORKFLOWB2B
    {

        public Int32 ID { get; set; }

        public string TITULO { get; set; }
        public string TIPO { get; set; }
        public string TIPO_SERVICIO { get; set; }
        public string IDCLIENTE { get; set; }
        public string DUI { get; set; }
        public string COTIZACION { get; set; }
        public string CELULA_TECNICA { get; set; }
        public string MAT_PRECIO { get; set; }
        public string MAT_CODIGO { get; set; }
        public string MAT_PROVEDOR { get; set; }
        public string MAT_LEADTIME { get; set; }
        public string MAT_DENSIDAD { get; set; }
        public string MAQ_INVERSION { get; set; }
        public string MAQ_LEADTIME { get; set; }
        public string ACC_INVERSION { get; set; }
        public string ACC_LEADTIME { get; set; }
        public string INS_PRECIO { get; set; }
        public string INS_CODIGO { get; set; }
        public string INS_LEADTIME { get; set; }
        public string PAR_PROCESO { get; set; }
        public string PAR_SETUP { get; set; }
        public string PAR_VELOCIDAD { get; set; }
        public string PA_DESPERDICIO_V { get; set; }
        public string PAR_DESPERDICIO_F { get; set; }

        public string UNIDAD_VENTA { get; set; }
        public string MARGEN { get; set; }
        public string LOTE_INDUSTRIAL { get; set; }

        public string CUST_PART_ID { get; set; }
        public string COMMODITY_CODE { get; set; }
        public string PROCESO_ACTUAL { get; set; }
        public int PASO_ACTUAL { get; set; }
        public string FECHA_INICIO { get; set; }
        public Decimal TIEMPO_TOTAL { get; set; }
        public Decimal TIEMPO_PROCESO { get; set; }
        public string FECHA_FINALIZACION { get; set; }
        public string NECESITA_MUESTRA { get; set; }
        public string FECHA_ESTIMADA { get; set; }
        public Int32 DIAS_TOTALES { get; set; }
        public string NOTAS { get; set; }
        public string ARCHIVO { get; set; }
        public string NOTASREQ { get; set; }
        public string INFORMACIONREQ { get; set; }
        public string USUARIO_INI { get; set; }
        public DateTime FECHA { get; set; }
        public List<string> CORREOS { get; set; }
        public string WORKFLOWS { get; set; }
    }
}