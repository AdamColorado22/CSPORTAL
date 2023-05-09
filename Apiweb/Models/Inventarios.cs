using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class Inventarios
    {
        public string PART_ID { get; set; }

        public string ITEM { get; set; }

        public decimal INVENTARIO { get; set; }

        public string DESCRIPCION { get; set; }
        public string FECHA { get; set; }
        public string CAJA { get; set; }
        public string TRACE_ID { get; set; }
        public string STAGE_ID { get; set; }
        public string CASE_QTY { get; set; }
    }
}