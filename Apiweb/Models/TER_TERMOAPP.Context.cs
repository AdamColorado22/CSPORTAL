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
    
    public partial class TERMOAPPEntities1 : DbContext
    {
        public TERMOAPPEntities1()
            : base("name=TERMOAPPEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TER_COM_PEDIDOS> TER_COM_PEDIDOS { get; set; }
        public virtual DbSet<TER_MATERIAL> TER_MATERIAL { get; set; }
        public virtual DbSet<TER_DET_COMERCIAL> TER_DET_COMERCIAL { get; set; }
    }
}
