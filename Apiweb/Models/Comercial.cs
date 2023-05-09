using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class Comercial
    {
        public Int32 ID { get; set; }
        public string PART_ID { get; set; }
        public string COTIZACION { get; set; }
        public string DUI { get; set; }
        public string ANCHO { get; set; }
        public string LARGO { get; set; }
        public string PESO_TOTAL { get; set; }
        public string PESO_MILLAR { get; set; }
        public string USUARIO { get; set; }
    }
}