using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Dashboard
{
    public class EnviosLogistica
    {
        public int Ordenamiento { get; set; }
        public string TipoLogistica { get; set; }
        public decimal Porcentaje { get; set; }
        public int TotEnvios { get; set; }
        public string NomEstado { get; set; }
        public string Color { get; set; }
        public string Tooltip { get; set; }
        public string TipoEnvio { get; set; }
    }
}