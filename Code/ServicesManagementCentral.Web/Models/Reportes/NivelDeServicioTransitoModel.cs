using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesManagement.Web.Models.Reportes
{
    public class NivelDeServicioTransitoModel
    {
        public string Transportista { get; set; }
        public int ConsignacionesTotales { get; set; }
        public int ConsignacionesRecolectadasEnTiempo { get; set; }
        public int ConsignacionesRecolectadasFueraTiempo { get; set; }
        public decimal NivelDeServicio { get; set; }
        public string Tipo { get; set; }
    }
}