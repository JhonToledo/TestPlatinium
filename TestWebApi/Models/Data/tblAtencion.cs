//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestWebApi.Models.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblAtencion
    {
        public int IDAtencion { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> Estado { get; set; }
        public int IDClienteCita { get; set; }
    }
}
