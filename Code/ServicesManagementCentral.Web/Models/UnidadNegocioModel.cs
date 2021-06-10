using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models
{
    public class UnidadNegocioModel
    {
        public Int16 Id_Num_UN { get; set; }
        public string Centros { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string CodigosPostales { get; set; }
    }
}