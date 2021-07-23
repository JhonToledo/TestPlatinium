using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApi.Models
{
    public class CitaViewModel
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int Estado { get; set; }
    }
}