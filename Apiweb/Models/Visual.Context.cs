﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VISUAL : DbContext
    {
        public VISUAL()
            : base("name=VISUAL")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<INVENTORY_TRANS> INVENTORY_TRANS { get; set; }
        public virtual DbSet<PART> PART { get; set; }
        public virtual DbSet<TRACE> TRACE { get; set; }
        public virtual DbSet<TRACE_INV_TRANS> TRACE_INV_TRANS { get; set; }
    }
}
