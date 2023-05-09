using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class Trazabilidad
    {
        
        public string BASE { get; set; }
        public string PART_ID { get; set; }
        public string VENDORPART { get; set; }
        public decimal INVENTARIO { get; set; }
        public string APROPERTY_1 { get; set; }
        public string DAYHAND { get; set; }
        public string TRACE_ID { get; set; }
        public string STAGE_ID { get; set; }
        public decimal PRECIO { get; set; }
        public string DESCRIPCION { get; set; }
    }
}