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
    
    public partial class TERMOEntities : DbContext
    {
        public TERMOEntities()
            : base("name=TERMOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TER_PORC_TEXTIL> TER_PORC_TEXTIL { get; set; }
        public virtual DbSet<TER_PORC_TEXTIL_HIST> TER_PORC_TEXTIL_HIST { get; set; }
    }
}
