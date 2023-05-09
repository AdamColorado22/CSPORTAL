using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class DetComercial
    {
        public Int32 ID { get; set; }
        public Int32 ID_COMERCIAL { get; set; }
        public string E_REP { get; set; }
        public string E_KG { get; set; }
        public string E_ROLL { get; set; }
        public string C_KG { get; set; }
        public string C_ROLLO { get; set; }
        public string PRECIO_M { get; set; }
        public string PRECIO_R { get; set; }
    }
}