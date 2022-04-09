using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Guias
{
    public class Cabecera
    {
        public string Consignacion { get; set; }
        public string Transportista { get; set; }
        public string Carrier { get; set; }
        public string TipoDeEnvio { get; set; }
        public string TipoDeServicio { get; set; }
        public int OrderNo { get; set; }
        public int IdTracking { get; set; }

    }
}