using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class PRECIOMH
    {
        public partial class PRECIO_MH
        {
            public int ID { get; set; }
            public string MH_NUMBER { get; set; }
            public string MHDATE { get; set; }
            public Nullable<int> LINE { get; set; }
            public string RELEASE_NUMBER { get; set; }
            public string ITEM { get; set; }
            public Nullable<decimal> PRECIO { get; set; }
            public Nullable<decimal> REQUES_QTY { get; set; }
            public Nullable<decimal> PRODUC_QTY { get; set; }
            public string PEDIDO_TERMO { get; set; }
            public string COMENTARIOS { get; set; }
        }
    }
}