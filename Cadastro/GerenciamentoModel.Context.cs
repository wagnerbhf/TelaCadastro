﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cadastro
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GerenciamentoContext : DbContext
    {
        public GerenciamentoContext()
            : base("name=GerenciamentoContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TB_Area> TB_Area { get; set; }
        public virtual DbSet<TB_Empresa> TB_Empresa { get; set; }
        public virtual DbSet<TB_Grupo> TB_Grupo { get; set; }
        public virtual DbSet<TB_GrupoPerfil> TB_GrupoPerfil { get; set; }
        public virtual DbSet<TB_Menu> TB_Menu { get; set; }
        public virtual DbSet<TB_Usuario> TB_Usuario { get; set; }
    }
}
