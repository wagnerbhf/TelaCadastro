//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TB_Empresa
    {
        public TB_Empresa()
        {
            this.TB_Area = new HashSet<TB_Area>();
            this.TB_Grupo = new HashSet<TB_Grupo>();
            this.TB_GrupoPerfil = new HashSet<TB_GrupoPerfil>();
            this.TB_Menu = new HashSet<TB_Menu>();
            this.TB_Usuario = new HashSet<TB_Usuario>();
        }
    
        public int IdEmpresa { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<TB_Area> TB_Area { get; set; }
        public virtual ICollection<TB_Grupo> TB_Grupo { get; set; }
        public virtual ICollection<TB_GrupoPerfil> TB_GrupoPerfil { get; set; }
        public virtual ICollection<TB_Menu> TB_Menu { get; set; }
        public virtual ICollection<TB_Usuario> TB_Usuario { get; set; }
    }
}
