using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class TerSolRecosteo
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int Paso { get; set; }
        public string Tipo { get; set; }
        public string Solicitud { get; set; }
        public string Respuesta { get; set; }
        public string UsuarioRespuesta { get; set; }
        public DateTime? FechaRespuesta { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}