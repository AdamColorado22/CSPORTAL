//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apiweb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TER_QW3_PRECIO_MH
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
