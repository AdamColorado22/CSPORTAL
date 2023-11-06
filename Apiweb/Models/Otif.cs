using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class Otif
    {
        public string BASE { get; set; }
        public string FECHA_INGRESO { get; set; }
        public string FECHA_PLANTA { get; set; }
        public string INVOICED_DATE { get; set; }
        public int? LEAD_TIME { get; set; }
        public int? ONTIME { get; set; }
        public string ORDENCOMPRA { get; set; }
        public string PART_ID { get; set; }
        public string UN { get; set; }
        public decimal? CANTIDAD { get; set; }
        public decimal? CANTIDAD_FACTURADA { get; set; }
        public decimal? DEUDA { get; set; }
        public decimal? INFULL { get; set; }


    }
}