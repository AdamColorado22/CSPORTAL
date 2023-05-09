using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Apiweb.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("ARCHIVOS")
        {
        }
        public IDbSet<Archivo> Archivos { get; set; }
    }
}